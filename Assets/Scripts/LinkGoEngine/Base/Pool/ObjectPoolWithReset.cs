using System;
using System.Collections.Generic;

namespace LinkGo.Base.Pool
{
    public interface IResetable
    {
        void Reset();
    }

    /// <summary>
    /// 用自定义类时，可以实现IResetable接口作为代替
    /// </summary>
    public class ObjectPoolWithReset<T> where T : class, IResetable, new()
    {
        private Stack<T> m_objectStack;

        public ObjectPoolWithReset(int capacity)
        {
            m_objectStack = new Stack<T>(capacity);
        }

        public T New()
        {
            if (m_objectStack.Count > 0)
            {
                T t = m_objectStack.Pop();
                t.Reset();
                return t;
            }
            else
            {
                return Activator.CreateInstance<T>();
            }
        }

        public void Store(T obj)
        {
            m_objectStack.Push(obj);
        }
    }
}

