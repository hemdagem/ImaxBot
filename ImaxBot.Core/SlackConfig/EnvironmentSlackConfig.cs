using System;
using ImaxBot.Core.SlackBot;

namespace ImaxBot.Core.SlackConfig
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