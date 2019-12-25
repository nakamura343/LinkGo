using UnityEngine;

namespace LinkGo.Common.Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T s_Instance;

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    return null;
                }

                if (s_Instance == null)
                {
                    s_Instance = (T)FindObjectOfType(typeof(T));

                    if (s_Instance == null)
                    {
                        GameObject singleton = new GameObject();
                        s_Instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }
                return s_Instance;
            }
        }

        private static bool applicationIsQuitting = false;

        public void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}


