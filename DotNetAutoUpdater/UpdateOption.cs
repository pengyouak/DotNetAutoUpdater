using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public static UpdateOption LoadUpdateOption(string xml)
        {
            XmlSerializer xs = XmlSerializerHelper.Create(typeof(UpdateOption));

            // XmlSerializer xs = XmlSerializer.FromTypes(new Type[] { typeof(UpdateOption) }).FirstOrDefault();

            // XmlSerializer xs = new XmlSerializer(typeof(UpdateOption));

            var sr = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            var config = xs.Deserialize(sr) as UpdateOption;
            sr.Close();

            return config;
        }

        public static void SaveUpdateOption(UpdateOption updateOption, string file)
        {
            XmlSerializer xs = XmlSerializerHelper.Create(typeof(UpdateOption));
            StreamWriter sw = new StreamWriter(file);
            xs.Serialize(sw, updateOption);
            sw.Close();
        }
    }
}