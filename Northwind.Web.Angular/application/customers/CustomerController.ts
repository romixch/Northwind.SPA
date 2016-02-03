/// <reference path="../../typings/angularjs/angular-resource.d.ts" />
/// <reference path="../Common.ts" />
/// <reference path="CustomerModels.ts" />
/// <reference path="CustomerResourceFactory.ts" />

module Northwind.Customers {
    'use strict';
    
    export class CustomerController {   
        numberOfEntries = [10, 20, 50, 100];     
        selectedNumberOfEntries: number;
        pageSize: number;
        customers: CustomerListModel[];
        links: Link[];
        pages: Pagination[] = [];
        
        static $inject = ["customerResourceFactory"];
        constructor(private customerResourceFactory: ICustomerResourceFactory) {
            this.selectedNumberOfEntries = this.numberOfEntries[0];
            this.getCustomers(0, this.selectedNumberOfEntries);
        }
        
        getCustomers = (page: number, pageSize: number) => {
            var resource = this.customerResourceFactory.getPagedCustomerListResource(page, pageSize);
            this.fetchData(resource);
        }
        
        reload = () => {
            this.getCustomers(0, this.selectedNumberOfEntries);
        }
        
        selectPage = (page: Pagination) => {
            this.getCustomers(page.PageNumber, page.PageSize);
        }
        
        previousPageDisabled = () : boolean => {
            return this.links.find(link => link.Rel == "prev") == undefined;
        }
        
        previousPage = () => {
            var uri = this.links.find(link => link.Rel == "prev").Href;
            var resource = this.customerResourceFactory.getCustomerListResourceWithUri(uri);
            this.fetchData(resource);
        }
        
        nextPageDisabled = () : boolean => {
            return this.links.find(link => link.Rel == "next") == undefined;
        }
        
        nextPage = () => {
            var uri = this.links.find(link => link.Rel == "next").Href;
            var resource = this.customerResourceFactory.getCustomerListResourceWithUri(uri);
            this.fetchData(resource);
        }
        
        private fetchData = (resource: ng.resource.IResourceClass<ICustomerListResource>) => {
            resource.get((data: CustomerListResponse) => {
                this.customers = data.Results;
                this.links = data.Links;
                this.pages = [];
                
                for (var n = 0; n < data.TotalPages; n++) {
                    this.pages.push(new Pagination(n, data.CurrentPage, data.PageSize));
                }
            });
        }
    }
}