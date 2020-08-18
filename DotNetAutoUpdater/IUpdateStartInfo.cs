using System.Diagnostics;

namespace DotNetAutoUpdater
{
    public interface IUpdateStartInfoHandler
    {
        ProcessStartInfo ParseStartInfo(ParseStartInfoArg arg);
    }
}