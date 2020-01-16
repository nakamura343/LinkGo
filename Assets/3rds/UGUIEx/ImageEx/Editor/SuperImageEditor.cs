using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(SuperImage), true)]
public class SuperImageEditor : ImageEditor
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


