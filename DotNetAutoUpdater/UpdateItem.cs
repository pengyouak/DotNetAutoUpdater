using System;

namespace DotNetAutoUpdater
{
    [Serializable]
    public class UpdateItem
    {
        public string Version { get; set; }

        public string Path { get; set; }

        public string ChangeLog { get; set; }
    }
}