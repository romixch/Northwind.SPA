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
    using System.Linq;

    using FakeItEasy;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public class ResourceCollectionTest
    {
        [Fact]
        public void IsResource()
        {
            var resources = CreateResources();
            var testee = new ResourceCollection<MyResource>(resources.Length, 1, 0, resources, new Link[0]);

            testee.Should().BeAssignableTo<Resource>();
        }

        [Fact]
        public void CanCreateFromJson()
        {
            var testee = CreateResourceCollectionFromJson();

            testee.Should().NotBeNull();
        }

        [Fact]
        public void ExposesGivenResults()
        {
            var testee = CreateResourceCollectionWithAllElements();

            testee.Results.Should().HaveCount(9);
        }

        [Fact]
        public void ExposesStatisticProperties_WhenAllResultsFitInOnePage()
        {
            var testee = CreateResourceCollectionWithAllElements();

            testee.TotalCount.Should().Be(9);
            testee.TotalPages.Should().Be(1);
            testee.CurrentPage.Should().Be(0);
            testee.PageSize.Should().Be(9);
        }

        [Fact]
        public void ContainsOnlySelfLink_WhenAllResultsFitInOnePage()
        {
            var testee = CreateResourceCollectionWithAllElements();
            testee.Links.Should().HaveCount(1).And.Contain(l => l.Rel == "self");
        }

        [Fact]
        public void ExposesStatisticProperties_WhenNotAllResultsFitInOnePage()
        {
            var testee = CreatePagedResourceCollection(1, 3);

            testee.TotalCount.Should().Be(9);
            testee.TotalPages.Should().Be(3);
            testee.CurrentPage.Should().Be(1);
            testee.PageSize.Should().Be(3);
        }

        [Fact]
        public void ContainsNextPageLink_WhenNotAllResultsFitInOnePageAndThereAreMorePages()
        {
            var testee = CreatePagedResourceCollection(0, 5);
            testee.Links.Should().HaveCount(2).And.Contain(l => l.Rel == "next");
        }

        [Fact]
        public void ContainsPreviousPageLink_WhenNotAllResultsFitInOnePageAndThisIsTheLastPage()
        {
            var testee = CreatePagedResourceCollection(1, 5);
            testee.Links.Should().HaveCount(2).And.Contain(l => l.Rel == "prev");
        }

        [Fact]
        public void ContainsLinksInBothDirections_WhenNotAllResultsFitInOnePageAndThereAreMorePagesAndThisIsNotTheLastPage()
        {
            var testee = CreatePagedResourceCollection(1, 2);
            testee.Links.Should().HaveCount(3)
                .And.Contain(l => l.Rel == "prev")
                .And.Contain(l => l.Rel == "next");
        }

        private static ResourceCollection<MyResource> CreateResourceCollectionFromJson()
        {
            return JsonConvert.DeserializeObject<ResourceCollection<MyResource>>(
                TestResources.PagedCustomerJsonResponse);
        }

        private static ResourceCollection<MyResource> CreateResourceCollectionWithAllElements()
        {
            var linkCreator = A.Fake<ICreateLinks>();
            A.CallTo(() => linkCreator.Create(A<object>.Ignored)).Returns("link/to/resource/");

            var resources = CreateResources();

            return new ResourceCollection<MyResource>(linkCreator, resources);
        }

        private static ResourceCollection<MyResource> CreatePagedResourceCollection(int page, int pageSize)
        {
            var linkCreator = A.Fake<ICreateLinks>();
            A.CallTo(() => linkCreator.Create(A<object>.Ignored)).Returns("link/to/resource/");

            var allResources = CreateResources();
            var pagedResources = CreateResources().Skip(pageSize * page).Take(pageSize);

            return new ResourceCollection<MyResource>(
                linkCreator, pagedResources, allResources.Length, page, pageSize);
        }

        private static MyResource[] CreateResources()
        {
            return new[]
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