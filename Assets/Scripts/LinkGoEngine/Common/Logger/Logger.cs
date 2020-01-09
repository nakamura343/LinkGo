using System.Collections.Generic;

namespace LinkGo.Common.Logger
{
    /// <summary>
    /// ������־����
    /// </summary>
    public enum LogType
    {
        None  = 0,  //����ȫ����־
        Trace = 1,  //�ر�ϸ�ڵ���־�����ܰ�����������Ϣ(����������)
        Debug = 2,  //Debug��Ϣ����Trace����Ϣ��Ҫ��һЩ��ͨ������������������ʹ�á�
        Info  = 4,  //��Ϣ��Ϣ������������������¾���ʹ�õ��ġ�
        Warn  = 8,  //������Ϣ��ͨ������ָʾ�ǹؼ��Ե����⣬��Щ������Ա��ָ�����ֻ����ʱ����
        Error = 16, //������Ϣ - �����ʱ�����Ƕ���Exception���쳣��
        Fatal = 32, //�ر����صĴ���
        All   = 127 //��ӡȫ����־
    }

    /// <summary>
    /// ��־��
    /// </summary>
    public class Logger
    {
        public string Tag { private set; get; }

        public LogType Type { private set; get; }

        public bool ShowTime { private set; get; }

        public List<BaseTarget> Targets { private set; get; }

        public Logger(string tag, LogType type, bool showTime)
        {
            Tag = tag;
            Type = type;
            ShowTime = showTime;
            Targets = new List<BaseTarget>();
        }

        public Logger AddTarget(BaseTarget target)
        {
            if (target != null)
            {
                Targets.Add(target);
            }
            return this;
        }

        public void Trace(string format, params object[] args)
        {
            if((Type & LogType.Trace) != 0)
            {
                Log(Tag, LogType.Trace, format, args);
            }
        }

        public void Debug(string format, params object[] args)
        {
            if ((Type & LogType.Debug) != 0)
            {
                Log(Tag, LogType.Debug, format, args);
            }
        }

        public void Info(string format, params object[] args)
        {
            if ((Type & LogType.Info) != 0)
            {
                Log(Tag, LogType.Info, format, args);
            }
        }

        public void Warn(string format, params object[] args)
        {
            if ((Type & LogType.Warn) != 0)
            {
                Log(Tag, LogType.Warn, format, args);
            }
        }

        public void Error(string format, params object[] args)
        {
            if ((Type & LogType.Error) != 0)
            {
                Log(Tag, LogType.Error, format, args);
            }
        }

        public void Fatal(string format, params object[] args)
        {
            if ((Type & LogType.Fatal) != 0)
            {
                Log(Tag, LogType.Fatal, format, args);
            }
        }

        private void Log(string tag, LogType type, string format, params object[] args)
        {
            string message = GetMessage(tag, type, format, args);
            foreach (var target in Targets)
            {
                target.OutputLog(type, message);
            }
        }

        private string GetMessage(string tag, LogType type, string format, params object[] args)
        {
            if(!ShowTime)
            {
                return LogManager.LogFormat(tag, type, format, args);
            }
            else
            {
                return LogManager.LogFormat2Time(tag, type, format, args);
            }
        }
    }
}


