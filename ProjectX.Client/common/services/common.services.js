(function () {
    'use strict';

    var common = angular.module('common.services', ["ngResource"]);
    common.constant("appSettings",
    {
        serverPath: "http://localhost:1835"
    });

})();