angularAppModule.factory('autoRepository', function ($http, $q) {
return {
get: function () {
     var deferred = $q.defer();
     $http.get('/api/Auto').success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     }
}});
