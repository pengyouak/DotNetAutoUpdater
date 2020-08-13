using System;

namespace DotNetAutoUpdater
{
    public static class AutoUpdaterExtensions
    {
        public static AutoUpdate Configure(this AutoUpdate autoUpdate, Func<UpdateRequestOption> func)
        {
            autoUpdate.UpdateRequestOption = func.Invoke();
            return autoUpdate;
        }
    }
}