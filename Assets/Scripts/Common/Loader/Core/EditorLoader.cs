#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace LinkGo.Common.Loader
{
    public class EditorLoader : BaseLoader
    {
        private Object m_Obj;

        public EditorLoader(string path) : base(path)
        {

        }

        public override void Start()
        {
            m_Obj = AssetDatabase.LoadMainAssetAtPath(Path);
            IsDone = true;
            Progress = 1f;
        }

        public override bool Update()
        {
            return true;
        }

        public override void End()
        {
            baseObject = new ResourceObject(m_Obj);
            onCompleted?.Invoke(baseObject);
        }
    }
}
#endif

