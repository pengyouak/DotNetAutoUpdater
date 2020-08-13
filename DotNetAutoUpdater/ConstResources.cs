using System.Collections.Generic;

namespace DotNetAutoUpdater
{
    public class ConstResources
    {
        public static string DefaultLang = "zh-cn";
        public static string Lang = "zh-cn";

        #region message

        public static readonly string UpdateJsonFileEmpty = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn","更新配置文件内容为空" }
            });

        public static readonly string UpdateJsonFileFormatInvalid = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn","更新配置文件反序列化失败" }
            });

        #endregion message

        #region control

        #region download diaglog

        public static readonly string ButtonTextShowDetail = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn", "显示详情" }
            });

        public static readonly string ButtonTextHideDetail = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn", "隐藏详情" }
            });

        #endregion download diaglog

        #region confirm diaglog

        public static readonly string LabelTextConfirmTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn", "发现新版本" }
            });

        public static readonly string LabelTextConfirmSubTitle = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn", "有新的版本已经发布，确定要下载并更新吗？" }
            });

        public static readonly string ButtonTextConfirmUpdate = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn", "更新" }
            });

        public static readonly string ButtonTextConfirmCancel = GetText(Lang, new Dictionary<string, string> {
                {"zh-cn", "取消" }
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