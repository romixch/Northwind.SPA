module Northwind {
    'use strict';
    
    export class Resource {
        Links: Link[];
    }
    
    export class ResourceCollection extends Resource {
        TotalCount: number;
        TotalPages: number;
        CurrentPage: number;
        PageSize: number;
    }
    
    export class Link {
        Rel: string;
        Href: string;
        Title: string;
    }
    
    export class Pagination {
        DisplayNumber: number;
        IsActive: boolean;
        PageNumber: number;
        PageSize: number;
        
        constructor(pageNumber: number, currentPage: number, pageSize: number) {
            this.DisplayNumber = pageNumber + 1;
            this.IsActive = (pageNumber == currentPage);
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}