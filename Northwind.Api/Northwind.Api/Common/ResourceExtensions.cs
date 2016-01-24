//-------------------------------------------------------------------------------
// <copyright file="ResourceExtensions.cs" company="frokonet.ch">
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

    public static class ResourceExtensions
    {
        public static IEnumerable<T> WithDetailLinks<T>(this IEnumerable<T> resources, Func<T, string> createLink) where T : Resource
        {
            var resourceList = resources as T[] ?? resources.ToArray();
            foreach (var resource in resourceList)
            {
                resource.AddLink(new DetailLink(createLink(resource), "Details"));
            }

            return resourceList;
        }
    }
}