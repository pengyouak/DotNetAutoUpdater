namespace DotNetAutoUpdater
{
    public interface IExecUpdateProvider
    {
        void ExecUpdate(AppUpdateArgs appUpdateArgs);
    }
}