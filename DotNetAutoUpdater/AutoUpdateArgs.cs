using System;

namespace DotNetAutoUpdater
{
    public class AutoUpdateArgs : EventArgs
    {
        public Uri Uri { get; set; }

        public string Message { get; set; }

        public bool IsUpdateAvailable { get; set; }

        public UpdateRequestOption UpdateRequestOption { get; set; }

        public UpdateOption UpdateOption { get; set; }

        public UpdateItem UpdateItem { get; set; }
    }
}