using System.Linq;
using ImaxBot.Core.FilmFinder;
using ImaxBot.Core.SlackConfig;
using Slackbot;

namespace ImaxBot.Core.SlackBot
{
    public class SlackBot
    {
        private readonly IFilmFinder _filmFinder;
        private readonly SlackConnectionInfo _slackConfig;

        public SlackBot(IFilmFinder filmFinder, ISlackConfig slackConfig)
        {
            _filmFinder = filmFinder;
            _slackConfig = slackConfig.GetConfig();
        }

        public void RunBot()
        {
            Bot bot = new Bot(_slackConfig.Token, _slackConfig.BotName);

            bot.OnMessage += async (sender, message) =>
            {
                if (message.MentionedUsers.Any(x => x == _slackConfig.BotName))
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
            return message.StartsWith("<") ? message.Remove(0, 13).Trim() : message.Trim();
        }
    }
}
