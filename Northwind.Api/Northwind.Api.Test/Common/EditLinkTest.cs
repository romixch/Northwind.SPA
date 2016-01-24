//-------------------------------------------------------------------------------
// <copyright file="EditLinkTest.cs" company="frokonet.ch">
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

    public class EditLinkTest
    {
        [Fact]
        public void IsLink()
        {
            var testee = new EditLink("link/to/resource/");

            testee.Should().BeAssignableTo<Link>();
        }

        [Fact]
        public void MapsRequiredArgumentsToProperties()
        {
            var testee = new EditLink("link/to/resource/");

            testee.Rel.Should().Be("edit");
            testee.Href.Should().Be("link/to/resource/");
            testee.Title.Should().BeNullOrEmpty();
        }

        [Fact]
        public void MapsAllArgumentsToProperties()
        {
            var testee = new EditLink("link/to/resource/", "Title");

            testee.Rel.Should().Be("edit");
            testee.Href.Should().Be("link/to/resource/");
            testee.Title.Should().Be("Title");
        }

        [Fact]
        public void ThrowsException_WhenHrefIsNull()
        {
            Action action = () => { new EditLink(null); };

            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ThrowsException_WhenHrefIsEmptyString()
        {
            Action action = () => { new EditLink(string.Empty); };

            action.ShouldThrow<ArgumentException>();
        }
    }
}