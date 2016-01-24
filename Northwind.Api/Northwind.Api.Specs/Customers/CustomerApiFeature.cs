//-------------------------------------------------------------------------------
// <copyright file="CustomerApiFeature.cs" company="frokonet.ch">
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
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using FluentAssertions;

    using Northwind.Api.Common;

    using Xbehave;

    public class CustomerApiFeature
    {
        [Scenario]
        public void GetAllCustomers(
            OwinApplication webService, 
            HttpResponseMessage message, 
            ResourceCollection<CustomerListModel> result)
        {
            "Given a web service"
                .f(() => webService = new OwinApplication(6161));

            "When requesting all customers"
                .f(async () => message = await webService.Client.GetAsync("api/customers"));

            "Then the status code of the HTTP message equals OK"
                .f(() => message.StatusCode.Should().Be(HttpStatusCode.OK));

            "Then the inner result can be parsed"
                .f(async () => result = await message.Content.ReadAsAsync<ResourceCollection<CustomerListModel>>());

            "Then there are some customers in the result"
                .f(() => result.Results.Count().Should().BeGreaterOrEqualTo(91));

            "Then there is a self link in the result"
                .f(() => result.Links.Should().Contain(l => l.Rel == "self"));
        }
    }
}