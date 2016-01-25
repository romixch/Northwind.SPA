/// <reference path="../typings/angularjs/angular.d.ts"/>
/// <reference path="../typings/angularjs/angular-route.d.ts"/>
/// <reference path="../typings/angularjs/angular-resource.d.ts" />

module Northwind {
    var app = angular.module('northwind', ['ngRoute', 'ngResource']);

    app.config([
        <any>'$routeProvider',
        ($routeProvider: angular.route.IRouteProvider) => {
            $routeProvider
                .when('/', { templateUrl: 'home/home.html' })
                .otherwise({ redirectTo: '/' });
        }
    ]);
}