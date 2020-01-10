using System;
using System.Collections.Generic;

namespace LinkGo.Base.Pool
{
    /// <summary>
    /// 有一些类型不需要在一系列帧中存留，仅在帧结束前就失效,可以在一个合适的时机将所有已经池化的对象(pooled objects)再次存储于池中。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPoolWithCollectiveReset<T> where T : class, new()
    {
        private List<T> m_objectList;
        private int m_nextAvailableIndex = 0;

        private Action<T> m_resetAction;
        private Action<T> m_onetimeInitAction;

        public ObjectPoolWithCollectiveReset(int initialBufferSize, Action<T>
            ResetAction = null, Action<T> OnetimeInitAction = null)
        {
            m_objectList = new List<T>(initialBufferSize);
            m_resetAction = ResetAction;
            m_onetimeInitAction = OnetimeInitAction;
        }

        public T New()
        {
            if (m_nextAvailableIndex < m_objectList.Count)
            {
                // an allocated object is already available; just reset it
                T t = m_objectList[m_nextAvailableIndex];
                m_nextAvailableIndex++;

                m_resetAction?.Invoke(t);

                return t;
            }
            else
            {
                // no allocated object is available
                T t = new T();
                m_objectList.Add(t);
                m_nextAvailableIndex++;

                m_onetimeInitAction?.Invoke(t);

                return t;
            }
        }

        public void ResetAll()
        {
            //重置索引
            m_nextAvailableIndex = 0;
        }
    }
}


