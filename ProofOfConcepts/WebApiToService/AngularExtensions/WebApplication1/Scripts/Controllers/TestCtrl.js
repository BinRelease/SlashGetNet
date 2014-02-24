(function () {
    'use strict';

    var controllerId = 'TestCtrl';

    // TODO: replace app with your module name
    angular.module('angularAppModule').controller(controllerId, function ($scope, testRepository) {
        console.log('testrepo: ');
        console.log(testRepository);
        $scope.title = 'TestCtrl';
        $scope.activate = activate;

        testRepository.getByID(15, true).then(function (fart) {
            console.log('fart success');
            console.log(fart);

            testRepository.postifyThisGuy(5, fart);
            testRepository.save(fart);
            testRepository.d(5);
        });

        function activate() { }
    });
})();
