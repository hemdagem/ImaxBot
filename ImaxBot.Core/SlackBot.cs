using Slackbot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Microsoft.Extensions.Configuration;

namespace ImaxBot.Core
{
    
    public class SlackBot : ISlackBot
    {
        private readonly IFilmFinder _filmFinder;
        private readonly Bot _bot;
        private readonly string _botName;
        public SlackBot(IConfiguration configuration, IFilmFinder filmFinder)
        {
            _filmFinder = filmFinder;
            _bot = new Bot(configuration["ImaxToken"], configuration["BotName"]);
            _botName = configuration["BotName"];
        }

        public void RunBot()
        {
            _bot.OnMessage += async (sender, message) =>
            {
                if (message.MentionedUsers.Any(x => x == _botName))
                {
                    string messageToSend = "";
                    FilmInformation document = await _filmFinder.Find(message.Text);

                    if (document == null)
                    {
                        _bot.SendMessage(message.Channel, "Film not found");
                    }
                    else
                    {
                        var filmDetails = await _filmFinder.GetFilmDetails(document.FilmId);
                        foreach (var filmDetail in filmDetails)
                        {
                            messageToSend += filmDetail.Title + "\r\n" + filmDetail.AuditoriumInfo + "\r\n";
                        }
                        _bot.SendMessage(message.Channel, messageToSend);

                    }
                }
            };
        }

    }

    public interface ISlackBot
    {
        void RunBot();
    }
}
