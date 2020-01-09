namespace LinkGo.Common.Download
{
    public enum DownloadState
    {
        Unknown     = 0,   
        UnDownload  = 1, //未开始下载
        Donwloading = 2, //正在下载
        Downloaded  = 3, //下载完成
        DownloadErr = 4, //下载失败
    }
}
