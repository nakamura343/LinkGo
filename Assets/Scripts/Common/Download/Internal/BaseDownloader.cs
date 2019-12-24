using System;
using System.Collections;
using UnityEngine.Networking;

namespace Quark.Common.Download
{
    public abstract class BaseDownloader
    {
        protected UnityWebRequest m_webRequest;

        /// <summary>
        /// 下载地址
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// 下载进度
        /// </summary>
        public float Progress { get; protected set; }

        /// <summary>
        /// 下载速度
        /// </summary>
        public float Speed { get; protected set; }

        /// <summary>
        /// 保存地址
        /// </summary>
        public string SavePath { get; protected set; }

        /// <summary>
        /// 下载状态
        /// </summary>
        public DownloadState State { get; protected set; }

        abstract public void Start();

        abstract public bool Update();

        abstract public void Stop();
    }
}
