namespace DotNetAutoUpdater
{
    public enum UpdateMode
    {
        /// <summary>
        /// 提示
        /// </summary>
        Prompt,

        /// <summary>
        /// 提示并显示更新通知
        /// </summary>
        PromptAndDetail,

        /// <summary>
        /// 强制更新
        /// </summary>
        Force
    }
}