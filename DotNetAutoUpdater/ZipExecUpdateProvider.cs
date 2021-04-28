using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DotNetAutoUpdater
{
    internal class ZipExecUpdateProvider : IExecUpdateProvider
    {
        public void ExecUpdate(AppUpdateArgs appUpdateArgs)
        {
            var items = XmlSerializerHelper.XmlDeSerializeObject<UpdateOption>(
               File.ReadAllText(Path.Combine(appUpdateArgs.TempFolderPath, appUpdateArgs.TempUpdateOption)));

            if (items == null)
            {
                MessageBox.Show(
                    ConstResources.UpdateNullUpdateOptionMessage,
                    ConstResources.UpdateNullUpdateOptionTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var zipFile = items.UpdateItems.Where(x => x.Path.ToLower().EndsWith(".zip")).ToList();
            var downloadPath = System.IO.Path.Combine(appUpdateArgs.TempFolderPath, appUpdateArgs.DownloadFolderName);
            foreach (var item in zipFile)
            {
                var fileFullName = System.IO.Path.Combine(downloadPath, item.Path);
                // 解压zip文件到相应的下载目录
                System.IO.Compression.ZipFile.ExtractToDirectory(fileFullName, downloadPath);
                using (var zipArchives = System.IO.Compression.ZipFile.OpenRead(fileFullName))
                {
                    foreach (var zipEntity in zipArchives.Entries)
                    {
                        // 跳过文件夹
                        if (zipEntity.FullName.EndsWith("/")) continue;
                        // 将解压出来的文件一一增加到更新文件目录的集合中
                        items.UpdateItems.Add(new UpdateItem() { Path = zipEntity.FullName });
                    }
                }
                // 删除压缩文件
                System.IO.File.Delete(fileFullName);

                // 将zip文件从更新文件目录中移除
                items.UpdateItems.Remove(item);
            }

            var dialog = new UpdateDialogs.UpdateDiaglog(appUpdateArgs, items);
            dialog.TopMost = true;
            dialog.ShowDialog();
        }
    }
}