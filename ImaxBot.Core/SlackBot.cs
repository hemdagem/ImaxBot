using System;
using Slackbot;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ImaxBot.Core
{

    public class SlackBot
    {
        private readonly IFilmFinder _filmFinder;

        public SlackBot(IFilmFinder filmFinder)
        {
            _filmFinder = filmFinder;
        }

        public void RunBot()
        {
            string botName = Environment.GetEnvironmentVariable("SLACK_BOT_NAME");
            Bot bot = new Bot(Environment.GetEnvironmentVariable("SLACK_TOKEN"), botName);

            bot.OnMessage += async (sender, message) =>
            {
                if (message.MentionedUsers.Any(x => x == botName))
                {
                    string messageToSend = "No times available yet for that film";

                    FilmInformation document = await _filmFinder.Find(CleanMessage(message.Text));

                    var filmDetails = await _filmFinder.GetFilmDetails(document.FilmId);

                    if (filmDetails.Count > 0)
                    {
                        messageToSend = "";
                        foreach (var detail in filmDetails)
                            messageToSend += $"{detail.Title} \r\n {detail.AuditoriumInfo} \r\n";
                    }

                    bot.SendMessage(message.Channel, messageToSend);
                }
            };
        }

        private string CleanMessage(string message)
        {
            if (message.StartsWith("<"))
                return message.Remove(0, 13).Trim();

            return message.Trim();
        }
    }
}
