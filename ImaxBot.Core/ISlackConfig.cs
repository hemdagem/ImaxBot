namespace ImaxBot.Core
{
    public interface ISlackConfig 
    {
        SlackConnectionInfo GetConfig();
    }
}