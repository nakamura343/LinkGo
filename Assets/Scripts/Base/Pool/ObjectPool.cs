using System;
using System.Collections.Generic;

namespace LinkGo.Base.Pool
{
    public class ObjectPool<T> where T : PooledObject, new()
    {
        private Stack<T> m_objectStack;

        public ObjectPool(int capacity)
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
                T t = new T();
                return t;
            }
        }

        public void Store(T obj)
        {
            m_objectStack.Push(obj);
        }
    }
}

