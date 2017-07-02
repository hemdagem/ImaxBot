﻿using System.IO;
using ImaxBot.Core;
using Microsoft.Extensions.Configuration;

namespace ImaxBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            var slackBot = new SlackBot(builder, new FilmFinder(new AngleSharpClient()));
            slackBot.RunBot();
            System.Console.Read();
        }
    }
}
