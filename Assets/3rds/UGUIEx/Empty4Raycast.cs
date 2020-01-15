/*
 * https://blog.uwa4d.com/archives/fillrate.html
 */
using UnityEngine.UI;

namespace UGUIEx
{
    public class Empty4Raycast : MaskableGraphic
    {
        protected Empty4Raycast() 
        {
            useLegacyMeshGeneration = false;
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();
        }
    }
}


