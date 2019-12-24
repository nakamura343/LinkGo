using System;

namespace Quark.Base.Utils
{
    public class Singleton<T> where T : new()
    {
        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }
        class SingletonCreator
        {
            internal static readonly T instance = new T();
        }
    }
}
