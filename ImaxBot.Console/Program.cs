using ImaxBot.Core;

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
