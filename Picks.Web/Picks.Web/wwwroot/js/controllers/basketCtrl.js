app.controller('basketCtrl', function ($scope, $http) {

    $http({
        method: 'GET',
        url: '/api/Basket/getBasketList'
    }).then(function successCallback(response) {

        $scope.images = response.data;
        $scope.basketCount = response.data.length;

    }, function errorCallback(response) {

       

    });
    

});

