using System;
using Slackbot;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ImaxBot.Core
{
    public class EnvironmentSlackConfig : ISlackConfig
    {
        public SlackConnectionInfo GetConfig()
        {
            return new SlackConnectionInfo
            {
                BotName = Environment.GetEnvironmentVariable("SLACK_BOT_NAME"),
                Token = Environment.GetEnvironmentVariable("SLACK_TOKEN")
            };
        }
    }
}