using System;
using System.IO;
using ImaxBot.Core;
using Microsoft.Extensions.Configuration;

namespace ImaxBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string botName = args[0];
            string token = args[1];
            if(botName == null)
            {
                throw new Exception("Bot Name is required");
            }
            if(token == null)
            {
                throw new Exception("Slack Token is required");
            }


            var slackBot = new SlackBot(botName,token, new FilmFinder(new AngleSharpClient()));
            slackBot.RunBot();
            System.Console.Read();
        }
    }
}
