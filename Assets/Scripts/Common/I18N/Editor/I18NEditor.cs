using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class I18NEditor : EditorWindow
{
    // SerializeField is used to ensure the view state is written to the window 
    // layout file. This means that the state survives restarting Unity as long as the window
    // is not closed. If the attribute is omitted then the state is still serialized/deserialized.
    [SerializeField] TreeViewState m_TreeViewState;

    //The TreeView is not serializable, so it should be reconstructed from the tree data.
    //I18NFileTreeView m_SimpleTreeView;

    void OnEnable()
    {
        // Check whether there is already a serialized view state (state 
        // that survived assembly reloading)
        if (m_TreeViewState == null)
            m_TreeViewState = new TreeViewState();

        //m_SimpleTreeView = new I18NFileTreeView(m_TreeViewState);

    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 200, position.height), EditorStyles.textField);
        {
            //m_SimpleTreeView.OnGUI(new Rect(0, 0, 200, position.height));

        }
        GUILayout.EndArea();

    }

    // Add menu named "My Window" to the Window menu
    static void ShowWindow()
    {
        // Get existing open window or if none, make a new one:
        var window = GetWindow<I18NEditor>();
        window.titleContent = new GUIContent("My Window");
        window.Show();
    }
}
