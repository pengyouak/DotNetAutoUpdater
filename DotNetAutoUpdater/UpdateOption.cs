using System;
using System.Collections.Generic;

namespace DotNetAutoUpdater
{
    public class UpdateOption
    {
        public bool IsUpdateAvailable { get; set; }

        public Version InstalledVersion { get; set; }

        public Version UpdateVersion { get; private set; }

        private string _version = "";

        public string Version
        {
            get => _version;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        this.UpdateVersion = new Version(value);
                        _version = value;
                    }
                    catch
                    {
                    }
                }
            }
        }

        public UpdateMode UpdateMode { get; set; }

        public string UpdateDetail { get; set; }

        public List<UpdateItem> UpdateItems { get; set; }
    }
}