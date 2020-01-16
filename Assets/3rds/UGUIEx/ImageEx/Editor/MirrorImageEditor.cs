using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(MirrorImage), true)]
public class MirrorImageEditor : ImageEditor
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


