//-------------------------------------------------------------------------------
// <copyright file="ResourceCollectionTest.cs" company="frokonet.ch">
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
    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public class ResourceCollectionTest
    {
        [Fact]
        public void IsResource()
        {
            var testee = CreateTestee();

            testee.Should().BeAssignableTo<Resource>();
        }

        [Fact]
        public void ExposesGivenResults()
        {
            var testee = CreateTestee();

            testee.Results.Should().HaveCount(9);
        }

        [Fact]
        public void ExposesStatisticProperties_WhenAllResultsFitInOnePage()
        {
            var testee = CreateTestee();

            testee.TotalCount.Should().Be(9);
            testee.TotalPages.Should().Be(1);
            testee.CurrentPage.Should().Be(0);
        }

        [Fact]
        public void ContainsOnlySelfLink_WhenAllResultsFitInOnePage()
        {
            var testee = CreateTestee();
            testee.Links.Should().HaveCount(1).And.Contain(l => l.Rel == "self");
        }

        [Fact]
        public void ExposesStatisticProperties_WhenNotAllResultsFitInOnePage()
        {
            var testee = CreateTestee(1, 3);

            testee.TotalCount.Should().Be(9);
            testee.TotalPages.Should().Be(3);
            testee.CurrentPage.Should().Be(1);
        }

        [Fact]
        public void ContainsNextPageLink_WhenNotAllResultsFitInOnePageAndThereAreMorePages()
        {
            var testee = CreateTestee(0, 5);
            testee.Links.Should().HaveCount(2).And.Contain(l => l.Rel == "next");
        }

        [Fact]
        public void ContainsPreviousPageLink_WhenNotAllResultsFitInOnePageAndThisIsTheLastPage()
        {
            var testee = CreateTestee(1, 5);
            testee.Links.Should().HaveCount(2).And.Contain(l => l.Rel == "prev");
        }

        [Fact]
        public void ContainsLinksInBothDirections_WhenNotAllResultsFitInOnePageAndThereAreMorePagesAndThisIsNotTheLastPage()
        {
            var testee = CreateTestee(1, 2);
            testee.Links.Should().HaveCount(3)
                .And.Contain(l => l.Rel == "prev")
                .And.Contain(l => l.Rel == "next");
        }

        private static ResourceCollection<MyResource> CreateTestee(int page = 0, int pageSize = 1000)
        {
            var linkCreator = A.Fake<ICreateLinks>();
            A.CallTo(() => linkCreator.Create(A<object>.Ignored)).Returns("link/to/resource/");

            var resources = new[]
            {
                new MyResource(1),
                new MyResource(2),
                new MyResource(3),
                new MyResource(4),
                new MyResource(5),
                new MyResource(6),
                new MyResource(7),
                new MyResource(8),
                new MyResource(9)
            };

            return new ResourceCollection<MyResource>(linkCreator, resources, page, pageSize);
        }

        private class MyResource : Resource
        {
            public MyResource(int value)
            {
                this.Value = value;
            }

            public int Value { get;  }
        }
    }
}