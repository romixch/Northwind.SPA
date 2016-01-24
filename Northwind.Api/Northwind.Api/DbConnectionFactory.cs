//-------------------------------------------------------------------------------
// <copyright file="DbConnectionFactory.cs" company="frokonet.ch">
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

namespace Northwind.Api
{
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private const string ConnectionStringName = "Northwind";
        
        public async Task<SqlConnection> CreateAsync()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            return connection;
        }
    }
}