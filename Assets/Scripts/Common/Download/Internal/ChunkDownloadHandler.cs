using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace LinkGo.Common.Download
{
    public class ChunkDownloadHandler : DownloadHandlerScript
    {
        //文件流，用来写入文件数据
        private FileStream m_fStream;

        //用作下载速度的时间统计
        private float m_LastTime = 0;

        //用来作为下载速度的大小统计
        private float m_LastDataSize = 0;

        //下载速度,单位:KB/S
        private float m_DownloadSpeed = 0f;
        public float DownloadSpeed
        {
            get
            {
                return m_DownloadSpeed*100%(1024 * 100);
            }
        }

        //要下载的文件总长度
        private int m_ContentLength = 0;
        private int ContentLength
        {
            get { return m_ContentLength; }
        }

        //已经下载的数据长度
        private int m_DownedLength = 0;
        public int DownedLength
        {
            get { return m_DownedLength; }
        }

        public float Progress
        {
            get
            {
                if(m_ContentLength != 0)
                {
                    return (float)m_DownedLength / m_ContentLength;
                }
                return 0f;
            }
        }

        //要保存的文件路径
        private string m_SavePath = null;


        /// <summary>
        /// 初始化下载句柄，定义每次下载的数据上限为200kb
        /// </summary>
        /// <param name="filePath">保存到本地的文件路径</param>
        public ChunkDownloadHandler(string filePath) : base(new byte[1024 * 200])
        {
            m_SavePath = filePath;
            this.m_fStream = new FileStream(filePath + ".temp", FileMode.Append, FileAccess.Write);    //文件流操作的是临时文件，结尾添加.temp扩展名
            m_DownedLength = (int)m_fStream.Length;  //设置已经下载的数据长度
        }

        /// <summary>
        /// 请求下载时，会先接收到文件的数据总量
        /// </summary>
        /// <param name="contentLength">如果是从网络上下载资源，则表示文件剩余下载的大小；如果是本地拷贝资源，则表示文件总长度</param>
        protected override void ReceiveContentLength(int contentLength)
        {
            m_ContentLength = contentLength + m_DownedLength;
            m_LastTime = Time.time;
            m_LastDataSize = m_DownedLength;
        }

        /// <summary>
        /// 当从网络接收数据时的回调，每帧调用一次
        /// </summary>
        /// <param name="data">接收到的数据字节流，总长度为构造函数定义的200kb，并非所有的数据都是新的</param>
        /// <param name="dataLength">接收到的数据长度，表示data字节流数组中有多少数据是新接收到的，即0-dataLength之间的数据是刚接收到的</param>
        /// <returns>返回true表示当下载正在进行，返回false表示下载中止</returns>
        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (data == null || data.Length == 0)
            {
                return false;
            }
            m_fStream.Write(data, 0, dataLength);
            m_DownedLength += dataLength;

            //统计下载速度
            if (Time.time - m_LastTime >= 1.0f)
            {
                m_DownloadSpeed = (m_DownedLength - m_LastDataSize) / (Time.time - m_LastTime);
                m_LastTime = Time.time;
                m_LastDataSize = m_DownedLength;
            }
            return true;
        }

        /// <summary>
        /// 所有数据接收完成的回调，将临时文件保存为制定的文件名
        /// </summary>
        protected override void CompleteContent()
        {
            string TempFilePath = m_fStream.Name;   //临时文件路径
            OnDispose();

            if (File.Exists(TempFilePath))
            {
                if (File.Exists(m_SavePath))
                {
                    File.Delete(m_SavePath);
                }
                File.Move(TempFilePath, m_SavePath);
            }
        }

        public void OnDispose()
        {
            m_fStream.Close();
            m_fStream.Dispose();
        }
    }
}
