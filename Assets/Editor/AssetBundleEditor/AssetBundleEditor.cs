using UnityEditor;

namespace ABBuildEditor
{
    public class AssetBundleEditor
    {
        #region Const
        public const string BUNDLE_ANDROID_DIR = "DataAndroid";
        public const string BUNDLE_IOS_DIR     = "DataiOS";
        public const string BUNDLE_PC_DIR      = "DataPC";


        #endregion

        #region LoadMode
        //Set CheckMark
        [MenuItem("AssetBundle/LoadMode/EditorLoader", true)]
        public static bool CheckLoadMode()
        {
            int loadMode = EditorPrefs.GetInt("LoadMode", 1);
            Menu.SetChecked("AssetBundle/LoadMode/EditorLoader",   loadMode == 1);
            Menu.SetChecked("AssetBundle/LoadMode/ResourceLoader", loadMode == 2);
            Menu.SetChecked("AssetBundle/LoadMode/BundleLoader",   loadMode == 3);
            return true;
        }

        [MenuItem("AssetBundle/LoadMode/EditorLoader")]
        public static void SwitchToEditorLoader()
        {
            SwitchToTargetMode(1);
        }

        [MenuItem("AssetBundle/LoadMode/ResourceLoader")]
        public static void SwitchToResourceLoader()
        {
            SwitchToTargetMode(2);
        }

        [MenuItem("AssetBundle/LoadMode/BundleLoader")]
        public static void SwitchToBundleLoader()
        {
            SwitchToTargetMode(3);
        }

        private static void SwitchToTargetMode(int loadMode)
        {
            EditorPrefs.SetInt("LoadMode", loadMode);
        }
        #endregion

        #region BuildAB
        [MenuItem("AssetBundle/BuildAB/BuildForAndroid")]
        public static void BuildForAndroid()
        {
            BuildForTargetPlatform(BuildTarget.Android);
        }

        [MenuItem("AssetBundle/BuildAB/BuildForiOS")]
        public static void BuildForiOS()
        {
            BuildForTargetPlatform(BuildTarget.iOS);
        }

        [MenuItem("AssetBundle/BuildAB/BuildForPC")]
        public static void BuildForPC()
        {
            BuildForTargetPlatform(BuildTarget.StandaloneWindows);
        }

        [MenuItem("AssetBundle/BuildAB/Clear")]
        public static void Clear()
        {

        }

        static void BuildForTargetPlatform(BuildTarget target)
        {

        }
        #endregion
    }
}

