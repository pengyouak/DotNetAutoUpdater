namespace DotNetAutoUpdater
{
    public interface IUpdateOptionHandler
    {
        UpdateOption ParseUpdateOption(string str);
    }
}