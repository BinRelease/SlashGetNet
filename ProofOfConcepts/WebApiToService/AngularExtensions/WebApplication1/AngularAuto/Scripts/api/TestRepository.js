angularAppModule.factory('testRepository', function ($http, $q) {
return {
get: function (name,isfart) {
     var deferred = $q.defer();
     $http.get('/api/Test').success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     },
post: function (randomnumber,fart) {
     var deferred = $q.defer();
     $http.post('/api/Test').success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     }
}});
