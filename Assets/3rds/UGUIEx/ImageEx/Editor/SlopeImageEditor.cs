using UnityEditor;
using UnityEditor.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(SlopeImage))]
public class SlopeImageEditor : ImageEditor
{
    SerializedProperty m_slopeAngle;
    SerializedProperty m_uvSlopeAngle;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_slopeAngle = serializedObject.FindProperty("m_slopeAngle");
        m_uvSlopeAngle = serializedObject.FindProperty("m_uvSlopeAngle");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(m_slopeAngle);
        EditorGUILayout.PropertyField(m_uvSlopeAngle);
        serializedObject.ApplyModifiedProperties();
    }
}

