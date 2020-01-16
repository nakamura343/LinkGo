using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UIEx/Empty4Raycast", 20)]
public class Empty4Raycast : MaskableGraphic
{
    protected Empty4Raycast()
    {
        useLegacyMeshGeneration = false;
    }

    /*
     * https://blog.uwa4d.com/archives/fillrate.html
     */
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
    }
}


