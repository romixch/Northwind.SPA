//-------------------------------------------------------------------------------
// <copyright file="OwinApplication.cs" company="frokonet.ch">
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
    using System;
    using System.Net.Http;

    using Microsoft.Owin.Hosting;

    using Owin;

    public class OwinApplication : IDisposable
    {
        private readonly IDisposable webApp;

        public OwinApplication(int portNumber)
        {
            var startOptionUrl = $"http://+:{portNumber}";
            var startOptions = new StartOptions(startOptionUrl) { ServerFactory = "Microsoft.Owin.Host.HttpListener" };

            this.webApp = WebApp.Start<Startup>(startOptions);
            this.Client = new HttpClient { BaseAddress = new Uri($"http://localhost:{portNumber}") };
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            this.webApp.Dispose();
        }
    }
}