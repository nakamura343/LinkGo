using LinkGo.Common.Resource;
using UnityEditor;

public static class PackBundle
{
    const string kSimulationMode = "AssetBundles/Simulation Mode";

    [MenuItem(kSimulationMode)]
    public static void ToggleSimulationMode()
    {
        ResourceManager.SimulateAssetBundleInEditor = !ResourceManager.SimulateAssetBundleInEditor;
    }

    [MenuItem(kSimulationMode, true)]
    public static bool ToggleSimulationModeValidate()
    {
        Menu.SetChecked(kSimulationMode, ResourceManager.SimulateAssetBundleInEditor);
        return true;
    }

    [MenuItem("AssetBundle/PackAll")]
    public static void PackAll()
    {

    }

    [MenuItem("AssetBundle/Clear")]
    public static void Clear()
    {

    }
}
