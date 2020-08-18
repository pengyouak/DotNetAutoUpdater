using System.Collections.Generic;

namespace DotNetAutoUpdater
{
    public class ConstResources
    {
        private static string DefaultLang = "zh-CN";
        public static string Lang = "zh-CN";

        #region message

        public static readonly string UpdateXmlFileEmpty = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN","更新配置文件内容为空" }
            });

        public static readonly string UpdateXmlFileNotFound = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN","更新配置未找到" }
            });

        public static readonly string UpdateXmlFileFormatInvalid = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN","更新配置文件反序列化失败" }
            });

        public static readonly string UpdateNullUpdateOptionTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN","更新失败" }
            });

        public static readonly string UpdateNullUpdateOptionMessage = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN","获取更新文件缓存失败" }
            });

        #endregion message

        #region control

        #region update diaglog

        public static readonly string FormTextUpdateTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "更新" }
            });

        public static readonly string LabelTextUpdateTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "正在更新" }
            });

        public static readonly string LabelTextUpdateSubTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "本次更新可能持续数分钟，在这期间您可以做其他的事。" }
            });

        public static readonly string LabelTextUpdateTotalProcess = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "更新进度" }
            });

        public static readonly string ButtonTextUpdateCancel = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "取消" }
            });

        #endregion update diaglog

        #region download diaglog

        public static readonly string FormTextDownloadTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "下载" }
            });

        public static readonly string LabelTextDownloadTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "正在下载更新" }
            });

        public static readonly string LabelTextDownloadSubTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "本次更新可能持续数分钟，在这期间您可以做其他的事。" }
            });

        public static readonly string LabelTextDownloadCurProcess = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "下载中..." }
            });

        public static readonly string LabelTextDownloadCurProcessFinished = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "下载完成" }
            });

        public static readonly string LabelTextDownloadTotalProcess = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "总进度" }
            });

        public static readonly string ButtonTextShowDetail = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "显示详情" }
            });

        public static readonly string ButtonTextHideDetail = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "隐藏详情" }
            });

        public static readonly string ButtonTextDownloadCancel = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "取消" }
            });

        public static readonly string ViewColTextDownloadFileName = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "文件名" }
            });

        public static readonly string ViewColTextDownloadUpdateVer = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "更新版本" }
            });

        public static readonly string ViewColTextDownloadChangeLog = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "更新内容" }
            });

        #endregion download diaglog

        #region confirm diaglog

        public static readonly string FormTextConfirmTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "提示" }
            });

        public static readonly string LabelTextConfirmTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "发现新版本" }
            });

        public static readonly string LabelTextConfirmSubTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "有新的版本已经发布，确定要下载并更新吗？" }
            });

        public static readonly string ButtonTextConfirmUpdate = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "更新" }
            });

        public static readonly string ButtonTextConfirmCancel = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "取消" }
            });

        public static readonly string ButtonTextConfirmRemind = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "稍后提醒" }
            });

        public static readonly string ButtonTextConfirmSkip = GetText(Lang, new Dictionary<string, string> {
                {"zh-CN", "跳过此版本" }
            });

        #endregion confirm diaglog

        #endregion control

        #region private methods

        private static string GetText(string lang, Dictionary<string, string> source)
        {
            if (source.ContainsKey(lang)) return source[lang].ToString();
            else return source[DefaultLang] ?? "undefined";
        }

        #endregion private methods
    }
}