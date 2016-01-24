//-------------------------------------------------------------------------------
// <copyright file="PreviousPageLinkTest.cs" company="frokonet.ch">
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

    using FluentAssertions;

    using Xunit;

    public class PreviousPageLinkTest
    {
        [Fact]
        public void IsLink()
        {
            var testee = new PreviousPageLink("link/to/previous/page/", "Previous Page");

            testee.Should().BeAssignableTo<Link>();
        }

        [Fact]
        public void MapsAllArgumentsToProperties()
        {
            var testee = new PreviousPageLink("link/to/previous/page/", "Previous Page");

            testee.Rel.Should().Be("prev");
            testee.Href.Should().Be("link/to/previous/page/");
            testee.Title.Should().Be("Previous Page");
        }

        [Fact]
        public void ThrowsException_WhenHrefIsNull()
        {
            Action action = () => { new PreviousPageLink(null, "Previous Page"); };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsException_WhenHrefIsEmptyString()
        {
            Action action = () => { new PreviousPageLink(string.Empty, "Previous Page"); };

            action.ShouldThrow<ArgumentException>();
        }
    }
}