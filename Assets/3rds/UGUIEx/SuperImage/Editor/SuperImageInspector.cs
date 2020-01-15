using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UGUIEx.SuperImage
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SuperImage), true)]
    public class SuperImageInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}


