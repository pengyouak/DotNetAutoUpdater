using System.Diagnostics;

namespace DotNetAutoUpdater
{
    public interface IUpdateStartInfoProvider
    {
        ProcessStartInfo ParseStartInfo(AppUpdateArgs arg);
    }
}