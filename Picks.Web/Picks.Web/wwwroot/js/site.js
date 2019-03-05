$(function () {

    //Image Upload
    $('#sumbit').click(function () {

        var input = document.getElementById('file');
        var files = input.files;
        var formData = new FormData();

        for (var i = 0; i !== files.length; i++) {
            formData.append("files", files[i]);
        }


        $.ajax({
            method: 'POST',
            url: 'api/ImageUpload/adImage',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {

                var imageurl = "/ImageUpload/" + response.imageUrl;
                $('#imgPreView').attr('src', imageurl);

                var model = {
                    Id: response.id,
                    FileName: response.fileName,
                    ImageUrl: response.imageUrl,
                    CategoryId: $('#categorySelectList').children(':selected').attr('id')
                };

                $.ajax({
                    method: 'POST',
                    url: 'api/ImageUpload/SaveImage',
                    contentType: 'application/json',
                    data: JSON.stringify(model),
                    success: function (data) {

                        swal({
                            title: "Bild uppladad!",
                            text: "Bilden blev uppladdad och finns i bild listan",
                            icon: "success"
                        });

                    }, error: function (data) {

                        swal({
                            title: "Något gick fel",
                            text: "Försök igen senare",
                            icon: "warning"
                        });

                    }
                });
            }, error: function (response) {

                swal({
                    title: "Något gick fel",
                    text: "Endast tillåtna filer (.jpg, .jpeg och .png)",
                    icon: "warning"
                });

            }
        });
    });

    //Get all images
    $.ajax({
        url: 'api/ImageUpload/getAllImages',
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            for (var i = 0; i < data.length; i++) {

                var imageurl = "/ImageUpload/" + data[i].imageUrl;
                $('#img-list').append('<div class="col-md-4">' + '<img src=' + imageurl + '>' + '</div>');
            }


        }, error: function (data) {

            swal({
                title: "Gick inte hämta bilder",
                text: "Gick inte hämta några bilder, försök igen.",
                icon: "warning"
            });

        }
    });


    //Get Categories
    $.ajax({
        url: 'api/Category/GetCategories',
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            var selectList = data;
            $.each(selectList, function (value, data) {
                $('#categorySelectList').append('<option id=' + data.id + '>' + data.name + '</option>');
            });

            for (var i = 0; i < data.length; i++) {
                $('#displayCategoryName').append('<p>' + data[i].name + '</p>');
            }
        }, error: function (data) {

            swal({
                title: "Gick inte hämta kategorier",
                text: "Gick inte hämta några kategorier, försök igen.",
                icon: "warning"
            });

        }
    });





    //Post Category
    $('#sumbitCategory').click(function () {

        var categoryName = $('#categoryName').val();
        console.log(categoryName.length);

        if (categoryName.length === 0) {



            swal({
                title: "Fel",
                text: "Namn är obligatorisk måste anges.",
                icon: "warning"
            });

            return;
        }


        var model = {
            Id: 0,
            Name: categoryName
        };

        $.ajax({
            url: 'api/Category/adCategory',
            method: 'POST',
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(model),
            success: function (data) {






                swal({
                    title: "Kategori skapad",
                    text: "Kategorien blev skapad och är nu synlig i listan.",
                    icon: "success"
                }).then(function () {

                    location.reload();


                });

            }, error: function (data) {


                console.log(data);


            }
        });
    });
});