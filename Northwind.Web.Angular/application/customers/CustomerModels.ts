/// <reference path="../Common.ts" />

module Northwind.Customers {
    'use strict';
    
    export class CustomerListResponse extends ResourceCollection {
        Results: CustomerListModel[];
    }
    
    export class CustomerListModel extends Resource {
        CustomerId: string;
        CompanyName: string;
        Address: string;
        City: string;
        PostalCode: string;
        Country: string;
    }
}