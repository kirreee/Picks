$(function () {


    //Get Categories
    $.ajax({
        url: 'api/Category/GetCategories',
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            var selectList = data;
            $.each(selectList, function (value, data) {
                $('#categorySelectList').append('<option>' + data.name + '</option>');
            });

            for (var i = 0; i < data.length; i++) {
                $('#displayCategoryName').append('<p>' + data[i].name + '</p>');
            }
        }, error: function (data) {
            console.log(data);
        }
    });

    //Post Category
    $('#sumbit').click(function () {

        var categoryName = $('#categoryName').val();
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
                
            }, error: function (data) {
                
            }
        });
    });
});