angularAppModule.factory('testRepository', function ($http, $q) {
return {
get: function (name,isfart) {
     var deferred = $q.defer();
     $http.get('/api/Test?name=' + name + '&isfart=' + isfart + '').success(deferred.resolve).error(deferred.reject);
     return deferred.promise;
     },
postifyThisGuy: function (randomnumber,fart) {
     var deferred = $q.defer();
     $http.post('/api/Test?randomnumber=' + randomnumber + '', fart).success(deferred.resolve).error(deferred.reject);
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
