﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();

            if (args.Length > 0)
            {
                hostBuilder.UseUrls(args[0]);
            }

            hostBuilder.Build().Run();
        }
    }
}
