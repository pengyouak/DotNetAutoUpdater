using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DotNetAutoUpdater
{
    [Serializable]
    public class UpdateOption
    {
        [XmlIgnore]
        public bool IsUpdateAvailable { get; set; }

        [XmlIgnore]
        public Version InstalledVersion { get; set; }

        [XmlIgnore]
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

        public string ServerUrl { get; set; }

        public UpdateMode UpdateMode { get; set; }

        public string ChangeLog { get; set; }

        public List<UpdateItem> UpdateItems { get; set; }
    }
}