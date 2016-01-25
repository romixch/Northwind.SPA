//-------------------------------------------------------------------------------
// <copyright file="CustomerController.cs" company="frokonet.ch">
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

namespace Northwind.Api.Customers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Northwind.Api.Common;
    
    [RoutePrefix("api/customers")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        private const int DefaultPage = 0;
        private const int MaxPageSize = 1000;

        private readonly ICustomerQuery customerQuery;

        public CustomerController(ICustomerQuery customerQuery)
        {
            this.customerQuery = customerQuery;
        }

        [Route("", Name = "All")]
        public async Task<HttpResponseMessage> Get(int page = DefaultPage, int pageSize = MaxPageSize)
        {
            var detailLink = new LinkCreator(this.Url, "CustomerDetails");
            var listLink = new LinkCreator(this.Url, "All");

            var customers = await this.customerQuery.FindAllAsync(
                c => detailLink.Create(new { customerId = c.CustomerId }));
            
            return Request.CreateResponse(
                HttpStatusCode.OK, 
                new ResourceCollection<CustomerListModel>(listLink, customers, page, pageSize));
        }

        [Route("{customerId}", Name = "CustomerDetails")]
        public Task<HttpResponseMessage> Get(string customerId)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}