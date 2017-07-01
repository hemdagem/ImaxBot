using Slackbot;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ImaxBot.Core
{

    public class SlackBot
    {
        private readonly IFilmFinder _filmFinder;
        private readonly Bot _bot;
        private readonly string _botName;
        public SlackBot(IConfiguration configuration, IFilmFinder filmFinder)
        {
            _filmFinder = filmFinder;
            _botName = configuration["SlackBotName"];
            _bot = new Bot(configuration["SlackToken"], _botName);
        }

        public void RunBot()
        {
            _bot.OnMessage += async (sender, message) =>
            {
                if (message.MentionedUsers.Any(x => x == _botName))
                {
                    string messageToSend = "";

                    FilmInformation document = await _filmFinder.Find(message.Text.Remove(0, 13).Trim());

                    var filmDetails = await _filmFinder.GetFilmDetails(document.FilmId);

                    if (filmDetails.Count > 0)
                    {
                        foreach (var detail in filmDetails)
                            messageToSend += (detail.Title + "\r\n" + detail.AuditoriumInfo + "\r\n");
                    }
                    else
                    {
                        messageToSend = "No times available yet for that film";
                    }

                    _bot.SendMessage(message.Channel, messageToSend);

                }
            };
        }

    }

}
