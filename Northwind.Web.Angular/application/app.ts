/// <reference path="../typings/angularjs/angular.d.ts" />
/// <reference path="../typings/angularjs/angular-route.d.ts" />
/// <reference path="../typings/angularjs/angular-resource.d.ts" />

/// <reference path="customers/CustomerController.ts" />
/// <reference path="customers/CustomerResourceFactory.ts" />

module Northwind {
    var app = angular.module('northwind', ['ngRoute', 'ngResource']);

    app.config([
        <any>'$routeProvider',
        ($routeProvider: angular.route.IRouteProvider) => {
            $routeProvider
                .when('/', { templateUrl: 'home/home.html' })
                .when('/customers', { templateUrl: 'customers/customers.html', controller: 'CustomerCtrl as vm' })
                .otherwise({ redirectTo: '/' });
        }
    ]);
    
    angular.module('northwind')
        .controller('CustomerCtrl', Northwind.Customers.CustomerController);
        
    angular.module('northwind')
        .service('customerResourceFactory', Northwind.Customers.CustomerResourceFactory);
}