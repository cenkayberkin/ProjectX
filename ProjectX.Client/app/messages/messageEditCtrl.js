(function () {
    'use strict';

    angular
        .module('app')
        .controller('MessageEditCtrl', messageEditCtrl);

    messageEditCtrl.$inject = ['messageResource', "$stateParams", "$state"];

    function messageEditCtrl(messageResource, $stateParams, $state) {
        /* jshint validthis:true */
        var vm = this;

        vm.VMmessage = "";
        
        var messageId = $stateParams.messageId;

        if (messageId == 0) {
            vm.title = "New Message";
        } else {
            messageResource.get({ id: messageId }, function (data) {
                vm.message = data;
                vm.title = "Message " + vm.message.subject;
            });
            console.log("Here is get message");
        }

        vm.editorOptions = {
            uiColor: '#000000'
        };

        vm.submit = function () {
            console.log(messageId);
            if (messageId == 0) {

                console.log(vm.message);
                messageResource.save(vm.message, function () {
                    vm.VMmessage = "Saved";
                });
            } else {
                vm.message.$update({ id: vm.message.id }, function () {
                    console.log("Updated");
                    vm.VMmessage = "Message Updated";
                }, function (error) {
                    console.log("could not update");
                });
            }
        };

        vm.cancel = function (editForm) {
            editForm.$setPristine();
            vm.message = {};
            vm.VMmessage = "";
            $state.go('messageList');
        };
        
    }
})();
