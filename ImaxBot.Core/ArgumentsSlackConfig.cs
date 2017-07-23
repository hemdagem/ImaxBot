using System;
using Slackbot;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ImaxBot.Core
{
    public class ArgumentsSlackConfig : ISlackConfig
    {
        private readonly string[] _args;
        public ArgumentsSlackConfig(string[] args)
        {
            _args = args;
        }

        public SlackConnectionInfo GetConfig()
        {
            return new SlackConnectionInfo
            {
                BotName = _args[0],
                Token = _args[1]
            };
        }
    }
}