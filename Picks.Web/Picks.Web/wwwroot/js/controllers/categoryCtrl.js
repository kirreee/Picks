app.controller('categoryCtrl', function ($scope, $http) {

    let categories;

    $http({
        method: 'GET',
        url: '/api/Category/GetCategories'
    }).then(function successCallback(response) {
        categories = response.data;
        $scope.categories = categories;
        $scope.isEmpty = false;
        if (categories.length <= 0) {
            $scope.isEmpty = true;
        }
    }, function errorCallback(response) {
    });


    

    $scope.adCategory = function () {
       
        var name = $('#categoryNameInput').val();
        if (name.length === 0) {
            $('#errorMsg').show();
            return;
        }
        var model = {
            Id: 0,
            Name: $scope.category.name
        };

        //Post category
        $http({
            method: 'POST',
            url: '/api/Category/adCategory',
            data: model
        }).then(function successCallback(response) {
            swal({
                title: 'Kategori skapad!',
                text: 'Kategorin är nu skapad och är synlig i listan',
                icon: 'success'
            }).then(function () {
                location.reload();
            });
        }, function errorCallback(response) {
            swal({
                title: 'OPS, något gick fel!',
                text: 'Gick inte skapa kategori försök igen',
                icons: 'warning'
            });
        });
    };
});