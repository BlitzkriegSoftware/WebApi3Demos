using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Blitz.WebAPI3Demo.Libs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blitz.WebAPI3Demo
{
    /// <summary>
    /// Entry Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args">Start-up Args</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create host builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #region "Program Metadata"

        private static Models.AssemblyVersionMetadata _programMetadata;

        /// <summary>
        /// Program Metadata (from Assembly)
        /// </summary>
        public static Models.AssemblyVersionMetadata ProgramMetadata
        {
            get
            {
                if(_programMetadata == null)
                {
                    _programMetadata = new Models.AssemblyVersionMetadata();
                    var assembly = typeof(Program).Assembly;
                    foreach(var attribute in assembly.GetCustomAttributesData())
                    {
                        if(!attribute.TryParse(out var value))
                        {
                            value = string.Empty;
                        }
                        var name = attribute.AttributeType.Name;
                        _programMetadata.PropertyGet(name, value);
                        System.Diagnostics.Trace.WriteLine($"Attribute: {name}={value}");
                    }
                }
                return _programMetadata;
            }
        }

        #endregion
    }
}
