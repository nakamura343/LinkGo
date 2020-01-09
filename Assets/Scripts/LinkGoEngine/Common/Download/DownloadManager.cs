using LinkGo.Common.Utils;
using System;
using System.Collections.Generic;

namespace LinkGo.Common.Download
{
    public class DownloadManager: MonoSingleton<DownloadManager>
    {
        private Dictionary<string, FileDownloader> m_DownloadingFileMap;

        private DownloadManager()
        {
            m_DownloadingFileMap = new Dictionary<string, FileDownloader>();
        }

        private void Update()
        {
            
        }

        public FileDownloader StartDownload(string url, string savePath, Action<float,float,int,int> ProgressCallback)
        {
            FileDownloader downloader;
            if (m_DownloadingFileMap.TryGetValue(url, out downloader))
            {
                return downloader;
            }

            downloader = new FileDownloader(url, savePath);
            //downloader.RegisteCompleteFunc(null);
            //downloader.RegisteProgressBack(ProgressCallback);


            m_DownloadingFileMap.Add(url, downloader);
            //downloader.StartDownload();
            return downloader;
        }

        public bool StopDownload(string url)
        {
            FileDownloader downloader;
            if (m_DownloadingFileMap.TryGetValue(url, out downloader))
            {
                //downloader.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
