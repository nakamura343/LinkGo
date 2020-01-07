//Ä¬ÈÏlogger
namespace LinkGo.Common.Logger
{
    public static class Log
    {
        private static readonly string s_Tag = "Unity";

        #region default
        public static void Trace(string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(s_Tag);
            if (log != null)
            {
                log.Trace(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", s_Tag));
            }
        }

        public static void Debug(string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(s_Tag);
            if (log != null)
            {
                log.Debug(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", s_Tag));
            }
        }

        public static void Info(string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(s_Tag);
            if (log != null)
            {
                log.Info(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", s_Tag));
            }
        }

        public static void Warn(string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(s_Tag);
            if (log != null)
            {
                log.Warn(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", s_Tag));
            }
        }

        public static void Error(string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(s_Tag);
            if (log != null)
            {
                log.Error(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", s_Tag));
            }
        }

        public static void Fatal(string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(s_Tag);
            if (log != null)
            {
                log.Fatal(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", s_Tag));
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
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", tag));
            }
        }

        public static void DebugByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Debug(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", tag));
            }
        }

        public static void InfoByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Info(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", tag));
            }
        }

        public static void WarnByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Warn(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", tag));
            }
        }

        public static void ErrorByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Error(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", tag));
            }
        }

        public static void FatalByTag(string tag, string format, params object[] args)
        {
            Logger log = LogManager.GetLogger(tag);
            if (log != null)
            {
                log.Fatal(format, args);
            }
            else
            {
                throw new System.Exception(string.Format("not found tag:{0} logger.", tag));
            }
        }
        #endregion
    }
}


