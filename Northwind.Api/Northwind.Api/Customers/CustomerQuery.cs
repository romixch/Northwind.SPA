//-------------------------------------------------------------------------------
// <copyright file="CustomerQuery.cs" company="frokonet.ch">
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
    using System;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using Dapper;

    using Northwind.Api.Common;

    public class CustomerQuery : ICustomerQuery
    {
        private readonly IDbConnectionFactory factory;

        public CustomerQuery(IDbConnectionFactory factory)
        {
            this.factory = factory;
        }

        public async Task<int> GetCustomerCountAsync()
        {
            using (var connection = await this.factory.CreateAsync().ConfigureAwait(false))
            {
                var customerCount = await connection
                    .QueryAsync<int>("SELECT COUNT(*) FROM dbo.Customers;")
                    .ConfigureAwait(false);

                return customerCount.Single();
            }
        }

        public async Task<CustomerListModels> FindAllAsync(Func<CustomerListModel, string> createDetailResource)
        {
            using (var connection = await this.factory.CreateAsync().ConfigureAwait(false))
            {
                var customers = await connection
                    .QueryAsync<CustomerListModel>("SELECT * FROM dbo.Customers;")
                    .ConfigureAwait(false);

                return new CustomerListModels(customers.WithDetailLinks(createDetailResource));
            }
        }

        public async Task<CustomerListModels> FindAsync(Func<CustomerListModel, string> createDetailResource, int page, int pageSize)
        {
            const string Sql = @"
            SELECT *
            FROM
            (
                SELECT ROW_NUMBER() OVER(ORDER BY CustomerId) AS RowNum, *
                FROM dbo.Customers
            ) AS RowConstrainedResult
            WHERE RowNum > @lowerBound AND RowNum <= @upperBound
            ORDER BY RowNum;";

            var lower = page * pageSize;
            var upper = lower + pageSize;
            using (var connection = await this.factory.CreateAsync().ConfigureAwait(false))
            {
                var customers = await connection
                    .QueryAsync<CustomerListModel>(Sql, new { lowerBound = lower, upperBound = upper })
                    .ConfigureAwait(false);

                return new CustomerListModels(customers.WithDetailLinks(createDetailResource));
            }
        }
    }
}