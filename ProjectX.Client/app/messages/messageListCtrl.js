(function () {
    'use strict';

    var app = angular.module('app');
    app.controller('MessageListCtrl', ["messageResource", messageListCtrl]);

    function messageListCtrl(messageResource) {
        /* jshint validthis:true */
        var vm = this;

        messageResource.query(function (data) {
            vm.Messages = data;
        });

        vm.title = 'Messages';
       

        vm.messageDelete = function (messageId) {
            vm.VMmessage = "Deleting ...";
            messageResource.delete({ id: messageId }, function (data) {
                vm.VMmessage = "Message " + data.subject + " deleted";
                vm.Messages = _.filter(vm.Messages, function (item) {

                    return item.id != data.id;
                });
                vm.message = data.address + " is removed ";
                //delete data.id from messages
            }, function (error) {
                vm.VMmessage = "Could not delete";
            });
   
        };
    }
})();
