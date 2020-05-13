using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UITestNetCore.Helpers
{
    public class BuilderJson
    {
        public static IConfigurationRoot configuration { get; set; } = null;

        public static string ReturnParameterAppSettings(string param)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();

            return configuration[param].ToString();
        }

      
    }
}
