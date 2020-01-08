using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

namespace ScriptTemplateEditor
{
    public class ScriptTemplateEditor : EditorWindow
    {
        static ScriptTemplateEditor instance;

        [MenuItem("Assets/Create/CreateScript", false, 80)]
        public static void CreateScript()
        {
            instance = EditorWindow.GetWindow<ScriptTemplateEditor>();
            instance.titleContent = new GUIContent("Create Script");
            instance.minSize = new Vector2(400, 100);
            instance.maxSize = new Vector2(400, 100);
            instance.Show();
        }

        ScriptTemplateSetting setting;

        int selectedIndex = 0;
        string scriptName = string.Empty;

        GUIContent[] displayOptions;
        List<string> templatePaths = new List<string>();
        List<TemplateMacro> macros = new List<TemplateMacro>();

        private void OnEnable()
        {
            setting = CommonEditorUtils.FindAsset<ScriptTemplateSetting>();
            if(setting != null)
            {
                macros.AddRange(setting.Macros);

                string[] files = Directory.GetFiles(setting.TemplatePath);
                displayOptions = new GUIContent[files.Length / 2];
                int index = 0;
                foreach (var file in files)
                {
                    if(file.Contains(".meta"))
                    {
                        continue;
                    }
                    templatePaths.Add(file);

                    FileInfo fileInfo = new FileInfo(file);
                    displayOptions[index] = new GUIContent(fileInfo.Name);
                    index++;
                }
            }

        }

        private void OnDisable()
        {
            templatePaths.Clear();
            macros.Clear();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            selectedIndex = EditorGUILayout.Popup(new GUIContent("Template:"), selectedIndex, displayOptions);
            scriptName = EditorGUILayout.TextField(new GUIContent("Name:"), scriptName);

            if (GUILayout.Button("Create"))
            {
                UnityEngine.Object select = Selection.activeObject;
                string output = AssetDatabase.GetAssetPath(select);

                string template = templatePaths[selectedIndex];
                string ext = Path.GetFileNameWithoutExtension(template);
                ext = ext.Substring(ext.IndexOf('.'));
                macros.Add(
                    new TemplateMacro()
                    {
                        key = "#DATE#",
                        value = DateTime.Now.ToString()
                    });
                macros.Add(
                    new TemplateMacro()
                    {
                        key = "#SCRIPTNAME#",
                        value = scriptName
                    });

                CreateScriptableObject(template, string.Format("{0}/{1}{2}", output, scriptName, ext));
            }

            EditorGUILayout.EndVertical();
        }

        public void CreateScriptableObject(string template, string scriptName)
        {
            if(!File.Exists(template))
            {
                return;
            }

            string fileContent = File.ReadAllText(template);

            foreach(var macro in macros)
            {
                fileContent = fileContent.Replace(macro.key, macro.value);
            }

            File.WriteAllText(scriptName, fileContent);
            AssetDatabase.Refresh();
            instance.Close();
        }
    }
}


