using System;
using System.IO;
using ImaxBot.Core;
using Microsoft.Extensions.Configuration;

namespace ImaxBot.Console
{
    class Program
    {
         public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            new SlackBot(new Configu).RunBot();

        }
    }
}
