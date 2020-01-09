using System;
using System.Text;
using System.Collections.Generic;

namespace LinkGo.Common.Logger
{
    public class LogManager
    {
        private static StringBuilder s_LogBuilder = new StringBuilder();

        private static Dictionary<string, Logger> s_LogMap = new Dictionary<string, Logger>();     

        public static void Init(LogConfig config)
        {
            if(config == null)
            {
                return;
            }
            List<LoggerElement> loggers = config.loggers;
            foreach(var logger in loggers)
            {
                Logger loggerObj = CreateLogger(logger);
                AddLogger(loggerObj);
            }
        }

        private static Logger CreateLogger(LoggerElement element)
        {
            Logger logger = new Logger(element.tag, (LogType)element.type, element.showTime);
            foreach(var targetEle in element.targets)
            {
                BaseTarget target = CreateTarget(targetEle);
                logger.AddTarget(target);
            }
            return logger;
        }

        private static BaseTarget CreateTarget(TargetElement element)
        {
            BaseTarget target;
            switch(element.type)
            {
                case TargetType.ConsoleTarget:
                    target = new ConsoleTarget();
                    break;
                case TargetType.FileTarget:
                    target = new FileTarget(element.path);
                    break;
                case TargetType.RemoteTarget:
                    target = new RemoteTarget(element.url);
                    break;
                default:
                    throw new Exception(string.Format("not fount type:{0} target.", element.type));
            }
            return target;
        }

        public static Logger GetDefaultLogger(string tag)
        {
            Logger log;
            if (!s_LogMap.TryGetValue(tag, out log))
            {
                //添加一个基本的logger
                log = new Logger(tag, LogType.All, true);
                log.AddTarget(new ConsoleTarget());
                s_LogMap.Add(tag, log);
            }
            return log;
        }

        public static Logger GetLogger(string tag) 
        {
            Logger log = null;
            s_LogMap.TryGetValue(tag, out log);
            return log;
        }

        public static void AddLogger(Logger logger)
        {
            if(logger == null)
            {
                return;
            }
            if(!s_LogMap.ContainsKey(logger.Tag))
            {
                s_LogMap.Add(logger.Tag, logger);
            }
        }

        public static void RemoveLogger(string tag)
        {
            if (s_LogMap.ContainsKey(tag))
            {
                s_LogMap.Remove(tag);
            }
        }

        public static string LogFormat(string tag, LogType type, string format, params object[] args)
        {
            s_LogBuilder.Clear();
            s_LogBuilder.AppendFormat("[Tag:{0}] [Type:{1}] >{2}", tag, type, string.Format(format, args));
            return s_LogBuilder.ToString();
        }

        public static string LogFormat2Time(string tag, LogType type, string format, params object[] args)
        {
            s_LogBuilder.Clear();
            s_LogBuilder.AppendFormat("[{0}] [Tag:{1}] [Type:{2}] >{3}", DateTime.Now, tag, type, string.Format(format, args));
            return s_LogBuilder.ToString();
        }
    }
}


