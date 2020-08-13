using System;

namespace DotNetAutoUpdater
{
    public class UpdateItem
    {
        public DateTime UpdateDate { get; set; }

        public string MinVersion { get; set; }

        public string Path { get; set; }

        public string ChangeLog { get; set; }

        public ValideMode ValideMode { get; set; }

        /// <summary>
        /// 本地是否校验文件的存在性
        /// </summary>
        public bool Required { get; set; }
    }
}