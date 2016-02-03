using System.Reflection;

using Microsoft.Owin;

using Northwind.Api;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Northwind.Api.WebHost")]
[assembly: AssemblyCompany("frokonet.ch")]
[assembly: AssemblyProduct("Northwind.Api")]
[assembly: AssemblyCopyright("Copyright ©  2016")]

[assembly: OwinStartup(typeof(Startup))]