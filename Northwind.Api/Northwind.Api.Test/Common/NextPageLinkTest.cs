//-------------------------------------------------------------------------------
// <copyright file="NextPageLinkTest.cs" company="frokonet.ch">
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

    public class NextPageLinkTest
    {
        [Fact]
        public void IsLink()
        {
            var testee = new NextPageLink("link/to/next/page/", "Next Page");

            testee.Should().BeAssignableTo<Link>();
        }

        [Fact]
        public void MapsAllArgumentsToProperties()
        {
            var testee = new NextPageLink("link/to/next/page/", "Next Page");

            testee.Rel.Should().Be("next");
            testee.Href.Should().Be("link/to/next/page/");
            testee.Title.Should().Be("Next Page");
        }

        [Fact]
        public void ThrowsException_WhenHrefIsNull()
        {
            Action action = () => { new NextPageLink(null, "Next Page"); };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsException_WhenHrefIsEmptyString()
        {
            Action action = () => { new NextPageLink(string.Empty, "Next Page"); };

            action.ShouldThrow<ArgumentException>();
        }
    }
}