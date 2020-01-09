using System.Collections.Generic;

namespace LinkGo.Base.Pool
{
    public class ObjectPool<T> where T : new()
    {
        private Stack<T> m_objectStack;

        public ObjectPool(int capacity)
        {
            m_objectStack = new Stack<T>(capacity);
        }

        public T Get()
        {
            if (m_objectStack.Count > 0)
            {
                T t = m_objectStack.Pop();
                return t;
            }
            else
            {
                return System.Activator.CreateInstance<T>();
            }
        }

        public void Release(T obj)
        {
            m_objectStack.Push(obj);
        }
    }
}

