namespace DotNetAutoUpdater
{
    public interface IUpdateOptionProvider
    {
        UpdateOption ParseUpdateOption(string str);
    }
}