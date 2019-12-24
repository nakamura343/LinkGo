using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Mongo.Common.I18N;

/// <summary>
/// I18 NB ehaviour inspector.
/// 
/// -------------------ReorderableList的回调函数-----------------
/// 1.drawElementCallback                                      
/// 2.drawHeaderCallback
/// 3.onReorderCallback
/// 4.onSelectCallback
/// 5.onAddCallback
/// 6.onAddDropdownCallback
/// 7.onRemoveCallback
/// 8.onCanRemoveCallback
/// 9.onChangedCallback
/// -----------------------------------------------------------
/// </summary>
[CustomEditor(typeof(I18NBehaviour))]
public class I18NBehaviourInspector : Editor
{
    protected ReorderableList _list;

    private void OnEnable()
    {
        _list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("languages"),
                true, true, true, true);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        _list.DoLayoutList();

        //1.绘制header的回调;
        _list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Language Array");
        };
        //绘制element的GUI回调;
        _list.drawElementCallback = DrawElementHandle;

        _list.onRemoveCallback = (ReorderableList l) =>
        {
            if (EditorUtility.DisplayDialog("Warning!",
                "Are you sure you want to delete the wave?", "Yes", "No"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
            }
        };

        _list.onAddCallback = (ReorderableList l) =>
        {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("language").enumValueIndex = 0;
            element.FindPropertyRelative("value").intValue = 20;
        };

        serializedObject.ApplyModifiedProperties();
    }

    public virtual void DrawElementHandle(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("language"), GUIContent.none);
        EditorGUI.PropertyField(
            new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("value"), GUIContent.none);
    }

}
