using LinkGo.Base.Pool;
using System;
using System.Diagnostics;

namespace LinkGo.Common.Utils
{
    public class TimerWatcher : IDisposable
    {
        private static ObjectPool<Stopwatch> s_watcherPool = new ObjectPool<Stopwatch>(8);

        private string m_Tag;
        private Stopwatch m_watcher;

        public TimerWatcher(string tag)
        {
            m_watcher = s_watcherPool.New();
            m_watcher.Start();
        }

        public void Dispose()
        {
            m_watcher.Stop();
            UnityEngine.Debug.LogFormat("<b>{0}</b> step cost time:<b>{1}</b>", m_Tag, m_watcher.ElapsedMilliseconds);
            m_watcher.Reset();
            s_watcherPool.Store(m_watcher);
        }
    }
}

