(function () {
    'use strict';

    var app = angular.module('app');
    app.controller('EmailGroupDetailCtrl', ["emailGroupResource", "$stateParams", "dataService", "Upload", "$scope", "appSettings",emailGroupDetailCtrl]);

    function emailGroupDetailCtrl(emailGroupResource, $stateParams, dataService, Upload, $scope, appSettings) {
        /* jshint validthis:true */
        var vm = this;

        var emailGroupId = $stateParams.emailGroupId;

        emailGroupResource.get({ id: emailGroupId }, function (data) {
            vm.emailGroup = data;
            vm.title = "Email group " + vm.emailGroup.name;
        });

        $scope.$watch('files', function () {
            $scope.upload($scope.files);
        });

        $scope.upload = function (files) {
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    Upload.upload({
                        url: appSettings.serverPath + '/api/emails/uploadfile/' + emailGroupId,
                        file: file
                    }).progress(function (evt) {
                        vm.message = "Uploading file";
                        console.log('progress started ');
                    }).success(function (data, status, headers, config) {
                        console.log('file ' + config.file.name + 'uploaded. Response: ' + data);
                        emailGroupResource.get({ id: emailGroupId }, function (data) {
                            vm.message = "";
                            vm.emailGroup = data;
                            vm.title = "Email group " + vm.emailGroup.name;
                        });
                    }).error(function () {
                        vm.message = "Error occured";
                    });
                }
            }
        };


        vm.emailDelete = function (selected) {
            //dataService.deleteEmail i cagir.
            vm.message = "Deleting ...";

            dataService.deleteEmailFromGroup(emailGroupId, selected.id)
                    .then(function (data) {
                        vm.emailGroup.emails = _.filter(vm.emailGroup.emails, function (item) {

                            return item.id != selected.id;
                        });
                        vm.message = data.address + " is removed ";

                    })
                    .catch(function () {
                        vm.message = "Problem occured";
                    });
        };
    }
})();
