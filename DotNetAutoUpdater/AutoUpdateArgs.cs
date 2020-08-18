using System;

namespace DotNetAutoUpdater
{
    public class AutoUpdateArgs : EventArgs
    {
        public string Message { get; set; }

        public UpdateContext UpdateContext { get; set; }
    }
}