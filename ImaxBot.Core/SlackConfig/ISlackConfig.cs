using ImaxBot.Core.SlackBot;

namespace ImaxBot.Core.SlackConfig
{
    public interface ISlackConfig 
    {
        SlackConnectionInfo GetConfig();
    }
}