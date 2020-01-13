#if UNITY_EDITOR
using UnityEditor;

namespace LinkGo.Common.Loader
{
    public class EditorLoader : BaseLoader
    {
        public EditorLoader(string path, int priority) : base(path, priority)
        {
        }

        public override void Start()
        {
        }

        public override bool Update()
        {
            IsDone = true;
            Progress = 1f;
            End();
            return true;
        }

        public override void End()
        {
            AssetObj = AssetDatabase.LoadMainAssetAtPath(Path);
            onCompleted?.Invoke(ELoaderType.EditorLoader, AssetObj);
        }
    }
}
#endif

