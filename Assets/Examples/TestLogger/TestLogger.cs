using LinkGo.Base.Utils;
using LinkGo.Common.Logger;
using UnityEngine;

public class TestLogger : MonoBehaviour
{
    private void Awake()
    {
        LogConfig config = XmlSerializeUtil.DeserializeFromFile<LogConfig>(Application.dataPath + @"/Examples/TestLogger/logconfig.xml");
        LogManager.Init(config);
    }

    // Start is called before the first frame update
    void Start()
    {
        //default + args
        Log.Trace("hello,{0}", "world");
        Log.Debug("hello,{0}", "world");
        Log.Info("hello,{0}", "world");
        Log.Warn("hello,{0}", "world");
        Log.Error("hello,{0}", "world");
        Log.Fatal("hello,{0}", "world");

        //tag + args
        Log.TraceByTag("qww", "hello,{0}", "world");
        Log.DebugByTag("qww", "hello,{0}", "world");
        Log.InfoByTag("qww", "hello,{0}", "world");
        Log.WarnByTag("qww", "hello,{0}", "world");
        Log.ErrorByTag("qww", "hello,{0}", "world");
        Log.FatalByTag("qww", "hello,{0}", "world");

        //no args
        Log.TraceByTag("zj", "You win some, you lose some");
        Log.DebugByTag("zj", "You win some, you lose some");
        Log.InfoByTag("zj", "You win some, you lose some");
        Log.WarnByTag("zj", "You win some, you lose some");
        Log.ErrorByTag("zj", "You win some, you lose some");
        Log.FatalByTag("zj", "You win some, you lose some");
    }
}
