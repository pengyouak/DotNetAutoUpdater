using System.Diagnostics;

namespace DotNetAutoUpdater
{
    public interface IUpdateStartInfoProvider
    {
        ProcessStartInfo ParseStartInfo(ParseStartInfoArg arg);
    }
}