//-------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="frokonet.ch">
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
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public abstract class Resource
    {
         private readonly IList<Link> links = new List<Link>();

        [JsonProperty(Order = 100)]
        public IEnumerable<Link> Links => this.links;

        public void AddLink(Link link)
        {
            Guard.NotNull(() => link);
            this.links.Add(link);
        }

        public void AddLinks(params Link[] linkParams)
        {
            linkParams.ToList().ForEach(this.AddLink);
        }
    }
}