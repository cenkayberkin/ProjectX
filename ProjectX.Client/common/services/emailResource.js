(function () {
    'use strict';

    var common = angular.module('common.services');
    common.factory('emailResource', emailResource);

    emailResource.$inject = ['$resource', 'appSettings'];

    function emailResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/emails/:id");
    }
})();