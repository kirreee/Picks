app.controller('homeCtrl', function ($scope, $http) {

    let images;
    let categories;

    //Get all categories to selectList
    $http({
        method: 'GET',
        url: '/api/Category/GetCategories'
    }).then(function successCallback(response) {
        let IsCategoriesEmpty = false;
        categories = response.data;

        if (categories.length <= 0) {
            IsCategoriesEmpty = true;
        }

        $scope.categories = categories;
    }, function errorCallback(response) {

    });

    //Get all images
    $http({
        method: 'GET',
        url: "/api/ImageUpload/getAllImages"
    }).then(function successCallback(response) {

        images = response.data;
        $scope.images = images;

    }, function errorCallback(response) {

    });

    //Post Image
    $scope.uploadImage = function () {
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

                var image = response;
                var model = {
                    Id: image.id,
                    FileName: image.fileName,
                    ImageUrl: image.imageUrl,
                    categoryId: $('#categorySelectList').children(':selected').attr('id')
                };

                $http({
                    method: 'POST',
                    url: '/api/ImageUpload/SaveImage',
                    contentType: 'application/json',
                    data: JSON.stringify(model)
                }).then(function successCallback(response) {
                    swal({
                        title: 'Bild uppladdad',
                        text: 'Bilden är nu uppladdad och är synlig i listan',
                        icon: 'success'
                    }).then(function () {
                        location.reload();
                    });
                }, function errorCallback(response) {
                    swal({
                        title: 'OPS, Något gick fel.',
                        text: 'Testa igen, endast tillåtna filtyper ( .jpg .jpeg .css .js )',
                        icon: 'warning'
                    });

                });
            }, error: function (response) {
                swal({
                    title: 'OPS, Något gick fel.',
                    text: 'Testa igen, endast tillåtna filtyper ( .jpg .jpeg .css .js )',
                    icon: 'warning'
                });
            }
        });
    };

    $scope.downloadImage = function (id) {
        
        var fileName = $('#categoryName' + id).val();
        var model = {
            FileName: fileName
        };

        

        $http({
            method: 'POST',
            url: '/api/ImageUpload/downloadImage',
            data: model
        }).then(function successCallback(response) {
            var hrefLink = "/ImagesZipFiles/" + response.data;
            window.location.href = hrefLink;
            swal({
                title: 'Bild nerladdad',
                text: 'Bilden blev nerladdad!',
                icon: 'success'
            });
        }, function errorCallback(response) {
            swal({
                title: 'OPS, något gick fel',
                text: 'Gick inte ladda ner bilden, försök igen',
                icon: 'warning'
            });
        });
    };

    //Add Images to basket.
    $scope.adImageToBasket = function (item) {
        var model = {
            FileName: item.fileName
        };

        $http({
            method: 'POST',
            url: '/api/Basket/adBasketItem',
            data: model
        }).then(function successCallback(response) {
            swal({
                title: 'Bild är tillagd i basket',
                icon: 'success'
            }).then(function () {
                location.reload();
            });
        }, function erorrCallback(response) {
            swal({
                title: 'Något gick fel',
                text: 'Bilden gick inte lägga till i basket försök igen',
                icons: 'warning'
            });
        });
    };

    $scope.startFiltrering = function () {
        var name = $('#selectListCategories :selected').text();
        console.log(name);
        
        var model = {
            CategoryName: name
        };


        $http({
            method: 'POST',
            url: '/api/ImageUpload/filteringImage',
            data: model
        }).then(function successCallback(response) {

            console.log(response.data);
            $scope.images = response.data;

        }, function errorCallback(response) {


        });

    };

});