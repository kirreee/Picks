app.controller('basketCtrl', function ($scope, $http, $timeout) {

    let Images;

    $http({
        method: 'GET',
        url: '/api/Basket/getBasketList'
    }).then(function successCallback(response) {

        Images = response.data;
        $scope.images = response.data;
        $scope.basketCount = response.data.length;

    }, function errorCallback(response) {

    });

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
            url: '/api/ImageUpload/downloadAllImagesFromBasket',
            data: JSON.stringify(imageArr)
        }).then(function successCallback(response) {
            var hrefLink = "/ImagesZipFiles/" + response.data;
            window.location.href = hrefLink;
            $timeout(function () {
                window.location.reload();
                }, 100);
            }, function errorCallback(response) {

                swal({
                    title: 'OPS..',
                    text: 'Somthing went wrong, try again',
                    icon: 'error'
                });

        });

    };


});

