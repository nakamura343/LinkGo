using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace UGUIEx.SuperImage
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SuperImage), true)]
    public class SuperImageInspector : ImageEditor
    {
        SerializedProperty _mirrorType;
        protected override void OnEnable()
        {
            base.OnEnable();
            _mirrorType = serializedObject.FindProperty("m_mirrorType");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(_mirrorType);
            serializedObject.ApplyModifiedProperties();
        }
    }
}


