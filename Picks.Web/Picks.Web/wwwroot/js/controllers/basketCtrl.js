app.controller('basketCtrl', function ($scope, $http, $timeout) {

    let Images;

    //Get all items from basket
    $http({
        method: 'GET',
        url: '/api/Basket/getBasketList'
    }).then(function successCallback(response) {
        Images = response.data;
        $scope.images = response.data;
        $scope.basketCount = response.data.length;
    }, function errorCallback(response) {
    });

    //Download all images
    $scope.downloadAllImages = function () {
        var images = Images;
        var imageArr = [];

        $('.fisk').each(function () {
            imageArr.push({
                fileName: $(this).val(),
                Id: $(this).attr('id')
            });
        });

        $http({
            method: 'POST',
            url: '/api/ImageUpload/downloadImagesFromBasket',
            data: JSON.stringify(imageArr)
        }).then(function successCallback(response) {
            var hrefLink = "/ImagesZipFiles/" + response.data;
            window.location.href = hrefLink;

            $timeout(function () {
                window.location.reload();
            }, 100);
        },
            function errorCallback(response) {
                swal({
                    title: 'OPS..',
                    text: 'Somthing went wrong, try again',
                    icon: 'error'
                });
            });
    };

    //Add active class to element
    $scope.selectetImage = function (event) {
        if ($(event.target).hasClass('active')) {
            $(event.target).removeClass('active');
            $(event.target).text('Select image to download');
            return;
        }

        $(event.target).addClass('active');
        $(event.target).text('Selected');
    };

    //Download selected images
    $scope.downloadTargetImage = function () {
        var images = [];
        $('.active').each(function () {
            images.push({
                fileName: $(this).attr('title'),
                Id: $(this).attr('id')
            });
        });

        $http({
            method: 'POST',
            url: '/api/ImageUpload/downloadImagesFromBasket',
            data: JSON.stringify(images)
        }).then(function successCallback(response) {
            var hrefLink = "/ImagesZipFiles/" + response.data;
            window.location.href = hrefLink;

            $timeout(function () {
                location.reload();
            }, 100);

        }, function errorCallback(response) {
            swal({
                title: 'OPS..',
                text: 'Somthing went wrong try again',
                icon: 'error'
            });
        });
    };

    //Remove all images from basket
    $scope.removeAllImageFromBasket = function () {
        var idsArray = [];
        $('.border-holder-image').each(function () {
            idsArray.push({
                Id: $(this).attr('id')
            });
        });

        $http({
            method: 'POST',
            url: '/api/Basket/RemoveAllItemsFromBasket',
            data: idsArray
        }).then(function successCallback(response) {
            swal({
                title: 'All images removed from basket',
                icon: 'success'
            }).then(function () {
                location.reload();
            });
        }, function errorCallback(response) {
            swal({
                title: 'OPS..',
                text: 'Somthing went wrong try again',
                icon: 'error'
            });
        });
    };

    //Remove image from basket
    $scope.removeImageFromBasket = function (id) {
        $http({
            method: 'DELETE',
            url: '/api/Basket/removeBasketItem/' + id
        }).then(function successCallback(response) {
            swal({
                title: 'Image removed from basket',
                icon: 'success'
            }).then(function () {
                location.reload();
            });
        }, function errorCallback(response) {
            swal({
                title: 'Somthing went wrong try again!',
                icon: 'error'
            });
        });
    };
});

