(function () {
    'use strict';

    angular
        .module('common.services')
        .factory('dataService', dataService);

    dataService.$inject = ['$q', '$http', 'appSettings'];

    function dataService($q, $http,appSettings) {
        var service = {
            mergeGroups: mergeGroups,
            deleteEmailFromGroup: deleteEmailFromGroup
        };

        return service;
        
        function deleteEmailFromGroup(groupId, emailId) {
            return $http({
                method: 'Delete',
                url: appSettings.serverPath + '/api/emails/' + groupId + '/' + emailId,
            })
                .then(function (response) {
                    return response.data;
                })
                .catch(function (response) {
                    return $q.reject('Error deleting email. ' + response.status);
                });
        }

        function mergeGroups(g1, g2) {
            return $http({
                method: 'Get',
                url: appSettings.serverPath + '/api/emailgroups/' + g1 + '/copygroups/' + g2,
            })
                .then(mergeSuccess)
                .catch(mergeError);
        }

        function mergeSuccess(response) {
            console.log(response);
            return 'Groups merged ';
        }

        function mergeError(response) {
            return $q.reject('Error merging groups. ' + response.status);
        }


    }
})();