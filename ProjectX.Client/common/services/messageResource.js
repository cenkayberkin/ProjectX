(function () {
    'use strict';

    var common = angular.module('common.services');
    common.factory('messageResource', messageResource);

    messageResource.$inject = ['$resource', 'appSettings'];

    function messageResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/messages/:id", null, {
            'update': { method: 'PUT' }
        });
    }
})();