//-------------------------------------------------------------------------------
// <copyright file="ResourceCollection.cs" company="frokonet.ch">
//   Copyright (c) 2016
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Northwind.Api.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public class ResourceCollection<T> : Resource
    {
        private const int DefaultPage = 0;

        private const int MaxPageSize = 1000;

        [JsonConstructor]
        public ResourceCollection(
            int totalCount,
            int totalPages,
            int currentPage,
            T[] results,
            Link[] links)
        {
            this.TotalCount = totalCount;
            this.TotalPages = TotalPages;
            this.CurrentPage = currentPage;
            this.Results = results;
            links.ToList().ForEach(this.AddLink);
        }

        public ResourceCollection(
            ICreateLinks links,
            IEnumerable<T> results, 
            int page = DefaultPage, 
            int pageSize = MaxPageSize)
        {
            var enumerable = results as T[] ?? results.ToArray();

            this.TotalCount = enumerable.Count();
            this.TotalPages = (int)Math.Ceiling((double)this.TotalCount / pageSize);
            this.CurrentPage = page;
            this.PageSize = pageSize;
            this.Results = enumerable.Skip(pageSize * page).Take(pageSize);

            this.AddPreviousPageLink(links, page, pageSize);
            this.AddNextPageLink(links, page, pageSize);
            this.AddSelfLink(links, page, pageSize);
        }

        public int TotalCount { get; }

        public int TotalPages { get; }

        public int CurrentPage { get; }

        public int PageSize { get; }

        public IEnumerable<T> Results { get;  }

        private void AddPreviousPageLink(ICreateLinks links, int page, int pageSize)
        {
            if (page > 0)
            {
                this.AddLink(new PreviousPageLink(links.Create(new { page = page - 1, pageSize }), "Previous Page"));
            }
        }

        private void AddNextPageLink(ICreateLinks links, int page, int pageSize)
        {
            if (page < this.TotalPages - 1)
            {
                this.AddLink(new NextPageLink(links.Create(new { page = page + 1, pageSize }), "Next page"));
            }
        }

        private void AddSelfLink(ICreateLinks links, int page, int pageSize)
        {
            this.AddLink(new SelfLink(links.Create(new { page, pageSize })));
        }
    }
}