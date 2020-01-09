using System;
using UnityEngine.Networking;

namespace LinkGo.Common.Download
{
    public class FileDownloader : BaseDownloader
    {
        private ChunkDownloadHandler m_DownloadHandler;

        public FileDownloader(string url, string savePath)
        {
            Url = url;
            State = DownloadState.UnDownload;
            SavePath = savePath;
        }

        public override void Start()
        {
            //断点续传设置读取文件数据流开始索引，成功会返回206
            m_webRequest = UnityWebRequest.Get(Url);
            m_webRequest.chunkedTransfer = true;
            m_webRequest.disposeDownloadHandlerOnDispose = true;
            m_webRequest.useHttpContinue = true; //默认就是true

            m_DownloadHandler = new ChunkDownloadHandler(SavePath);
            m_webRequest.downloadHandler = m_DownloadHandler;
            m_webRequest.SetRequestHeader("Range", "bytes=" + m_DownloadHandler.DownedLength + "-");

            m_webRequest.SendWebRequest(); //协程操作，可以在协程中调用等待
            State = DownloadState.Donwloading;
        }

        public override bool Update()
        {
            if(m_webRequest.isHttpError || m_webRequest.isNetworkError || !string.IsNullOrEmpty(m_webRequest.error))
            {
                State = DownloadState.DownloadErr;
                return true;
            }

            if(m_webRequest.isDone)
            {
                State = DownloadState.Downloaded;
                return true;
            }
            return false;
        }

        public override void Stop()
        {
            if (m_DownloadHandler != null)
            {
                m_DownloadHandler.OnDispose();  //释放文件操作的资源
            }

            if (m_webRequest != null)
            {
                m_webRequest.Abort();    //中止下载
                m_webRequest.Dispose();  //释放
            }
        }
    }
}
