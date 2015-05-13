(function () {
    'use strict';

    var common = angular.module('common.services');
    common.factory('emailGroupResource', emailGroupResource);

    emailGroupResource.$inject = ['$resource','appSettings'];

    function emailGroupResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/emailgroups/:id");
    }
})();