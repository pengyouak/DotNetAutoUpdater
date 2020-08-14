using System.IO;

namespace DotNetAutoUpdater
{
    internal class UpdateToolOption
    {
        public int PID { get; set; }

        public string AppName { get; private set; }

        private string _appFullPath = "";

        public string AppFullPath
        {
            get => _appFullPath;
            set
            {
                try
                {
                    var fileInfo = new FileInfo(value);
                    if (File.Exists(fileInfo.FullName)) this.AppName = fileInfo.Name;

                    _appFullPath = value;
                }
                catch { }
            }
        }

        public UpdateOption UpdateOption { get; set; }
    }
}