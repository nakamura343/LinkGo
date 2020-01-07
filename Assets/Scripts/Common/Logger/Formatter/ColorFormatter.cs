
namespace LinkGo.Common.Logger
{
    public class ColorFormatter
    {
        public static readonly string s_TraceColor = "#000000FF";
        public static readonly string s_DebugColor = "#B8FF00FF";
        public static readonly string s_InfoColor  = "#00FF4DFF";
        public static readonly string s_WarnColor  = "#FFBB00FF";
        public static readonly string s_ErrorColor = "#FF3939FF";
        public static readonly string s_FatalColor = "#FF0000FF";

        public static string LogFormat(LogType type, string log)
        {
            string color;
            switch(type)
            {
                case LogType.Trace:
                    color = s_TraceColor;
                    break;
                case LogType.Debug:
                    color = s_DebugColor;
                    break;
                case LogType.Info:
                    color = s_InfoColor;
                    break;
                case LogType.Warn:
                    color = s_WarnColor;
                    break;
                case LogType.Error:
                    color = s_ErrorColor;
                    break;
                case LogType.Fatal:
                    color = s_FatalColor;
                    break;
                default:
                    color = s_TraceColor;
                    break;
            }
            return string.Format("<color={0}>{1}</color>", color, log);
        }
    }
}


