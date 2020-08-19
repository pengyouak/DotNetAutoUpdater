using System.Xml.Serialization;

namespace DotNetAutoUpdater
{
    public enum UpdateMode
    {
        /// <summary>
        /// 提示
        /// </summary>
        [XmlEnum("0")]
        Prompt,

        /// <summary>
        /// 提示并显示更新通知
        /// </summary>
        [XmlEnum("1")]
        PromptAndDetail,

        /// <summary>
        /// 强制更新
        /// </summary>
        [XmlEnum("2")]
        Force
    }
}