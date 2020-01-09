//Ä¬ÈÏlogger
namespace LinkGo.Common.Logger
{
    public static class Log
    {
        private static readonly string s_Tag = "Unity";

        #region default
        public static void Trace(string format, params object[] args)
        {
            Logger log = LogManager.GetDefaultLogger(s_Tag);
            if (log != null)
            {
                log.Trace(format, args);
            }
        }

        public static void Debug(string format, params object[] args)
        {
            Logger log = LogManager.GetDefaultLogger(s_Tag);
            if (log != null)
            {
                log.Debug(format, args);
            }
        }

        public static void Info(string format, params object[] args)
        {
            Logger log = LogManager.GetDefaultLogger(s_Tag);
            if (log != null)
            {
                log.Info(format, args);
            }
        }

        public static void Warn(string format, params object[] args)
        {
            Logger log = LogManager.GetDefaultLogger(s_Tag);
            if (log != null)
            {
                log.Warn(format, args);
            }
        }

        public static void Error(string format, params object[] args)
        {
            Logger log = LogManager.GetDefaultLogger(s_Tag);
            if (log != null)
            {
                log.Error(format, args);
            }
        }

        public static void Fatal(string format, params object[] args)
        {
            Logger log = LogManager.GetDefaultLogger(s_Tag);
            if (log != null)
            {
                log.Fatal(format, args);
            }
        }
        #endregion

        #region tag
        public static void TraceByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if(log != null)
            {
                log.Trace(format, args);
            }
        }

        public static void DebugByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Debug(format, args);
            }
        }

        public static void InfoByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Info(format, args);
            }
        }

        public static void WarnByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Warn(format, args);
            }
        }

        public static void ErrorByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Error(format, args);
            }
        }

        public static void FatalByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Fatal(format, args);
            }
        }
        #endregion
    }
}


