using ImaxBot.Core;

namespace ImaxBot.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var slackBot = new SlackBot(new FilmFinder(new AngleSharpClient()));
            slackBot.RunBot();
            System.Console.Read();
        }
    }
}
