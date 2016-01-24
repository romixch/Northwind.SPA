//-------------------------------------------------------------------------------
// <copyright file="ICreateLinks.cs" company="frokonet.ch">
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
    using System.Web.Http.Routing;

    public interface ICreateLinks
    {
        string Create(object routeValues);
    }

    public class LinkCreator : ICreateLinks
    {
        private readonly UrlHelper urlHelper;

        private readonly string routeName;

        public LinkCreator(UrlHelper urlHelper, string routeName)
        {
            this.urlHelper = urlHelper;
            this.routeName = routeName;
        }

        public string Create(object routeValues)
        {
            return this.urlHelper.Link(this.routeName, routeValues);
        }
    }
}