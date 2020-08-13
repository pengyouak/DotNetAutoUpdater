using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotNetAutoUpdater
{
    public class UpdateOption
    {
        [JsonIgnore]
        public bool IsUpdateAvailable { get; set; }

        [JsonIgnore]
        public Version InstalledVersion { get; set; }

        [JsonIgnore]
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

        public string ChangeLog { get; set; }

        public List<UpdateItem> UpdateItems { get; set; }
    }
}