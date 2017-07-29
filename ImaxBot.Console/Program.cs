using ImaxBot.Core;
using ImaxBot.Core.AngleSharpClient;
using ImaxBot.Core.FilmFinder;
using ImaxBot.Core.SlackBot;
using ImaxBot.Core.SlackConfig;

namespace ImaxBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ISlackConfig slackConfig = args.Length > 0 ? (ISlackConfig) new ArgumentsSlackConfig(args) : (ISlackConfig) new EnvironmentSlackConfig();
            var slackBot = new SlackBot(new FilmFinder(new AngleSharpClient()),slackConfig);
            slackBot.RunBot();
            System.Console.Read();
        }
    }
}
