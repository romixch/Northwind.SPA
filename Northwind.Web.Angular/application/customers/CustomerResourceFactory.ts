/// <reference path="../../typings/angularjs/angular-resource.d.ts" />
/// <reference path="CustomerModels.ts" />

module Northwind.Customers {
    'use strict';
    
    export interface ICustomerListResource extends ng.resource.IResource<CustomerListModel> {
    }
    
    export interface ICustomerResourceFactory {
        getCustomerListResource(): ng.resource.IResourceClass<ICustomerListResource>;
        getCustomerListResourceWithUri(uri: string): ng.resource.IResourceClass<ICustomerListResource>;
        getPagedCustomerListResource(page: number, pageSize: number): ng.resource.IResourceClass<ICustomerListResource>;
    }
    
    export class CustomerResourceFactory implements ICustomerResourceFactory {
        static $inject = ["$resource"];
        constructor(private $resource: ng.resource.IResourceService) {   
        }
        
        getCustomerListResource = (): ng.resource.IResourceClass<ICustomerListResource> => {
            return this.$resource("/api/customers");
        }
        
        getCustomerListResourceWithUri = (uri: string): ng.resource.IResourceClass<ICustomerListResource> => {
            return this.$resource(uri);
        }
        
        getPagedCustomerListResource = (page: number, pageSize: number): ng.resource.IResourceClass<ICustomerListResource> => {
            return this.$resource("/api/customers?page=" + page + "&pageSize=" + pageSize);
        }
    }
}