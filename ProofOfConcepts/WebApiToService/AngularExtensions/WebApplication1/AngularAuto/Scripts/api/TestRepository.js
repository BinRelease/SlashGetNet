angularAppModule.factory('testRepository', function ($http, $q) {
return {
get: function (id,isfart) {
     var deferred = $q.defer();
     $http.get('/api/Test/' + id + '?isfart=' + isfart + '').success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     },
get: function () {
     var deferred = $q.defer();
     $http.get('/api/Test').success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     },
postifyThisGuy: function (id,fart) {
     var deferred = $q.defer();
     $http.post('/api/Test/' + id + '', fart).success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     },
save: function (fart) {
     var deferred = $q.defer();
     $http.put('/api/Test', fart).success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     },
d: function (id) {
     var deferred = $q.defer();
     $http.delete('/api/Test/' + id + '').success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     }
}});
