(function () {
    'use strict';

    var app = angular.module('app');
    app.controller('EmailGroupMergeCtrl', ["emailGroupResource", "dataService", "$state", emailGroupMergeCtrl]);

    function emailGroupMergeCtrl(emailGroupResource, dataService, $state) {
        /* jshint validthis:true */
        var vm = this;
        vm.message = "Loading ...";

        vm.selectLoading = true;
        
        emailGroupResource.query(function (data) {
            vm.firstGroupList = data;
            vm.firstGroup = vm.firstGroupList[0];
            vm.secondGroupList = angular.copy(vm.firstGroupList);

            vm.secondGroupList = _.filter(vm.firstGroupList, function (item) {
                return item.id != vm.firstGroup.id;
            });
            vm.secondGroup = vm.secondGroupList[0];
            vm.message = "";
            vm.selectLoading = false;
            
        });

        vm.updateSecondList = function () {
            vm.secondGroupList = angular.copy(vm.firstGroupList);

            vm.secondGroupList = _.filter(vm.firstGroupList, function (item) {
                return item.id != vm.firstGroup.id;
            });
            vm.secondGroup = vm.secondGroupList[0];
        };

        vm.title = 'Copy group into another';

        vm.submit = function () {
            vm.message = "Copying ...";
            dataService.mergeGroups(vm.firstGroup.id,vm.secondGroup.id)
                    .then(function () {
                        vm.message = "Copying Success";
                    })
                    .catch(function () {
                        vm.message = "Problem occured";
                    });
        };

        vm.cancel = function () {
            vm.message = "";
            $state.go('emailGroupList');
        };
    }
})();
