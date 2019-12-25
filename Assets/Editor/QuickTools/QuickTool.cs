using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class QuickTool : EditorWindow
{
    [MenuItem("Window/UIElements/QuickTool")]
    public static void ShowExample()
    {
        QuickTool wnd = GetWindow<QuickTool>();
        wnd.titleContent = new GUIContent("QuickTool");
        wnd.minSize = new Vector2(250, 50);
    }

    public void OnEnable()
    {
        // Reference to the root of the window.
        var root = rootVisualElement;

        // Associates a stylesheet to our root. Thanks to inheritance, all root¡¯s
        // children will have access to it.
        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/QuickTools/QuickTool.uss"));

        // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
        var quickToolVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/QuickTools/QuickTool.uxml");
        quickToolVisualTree.CloneTree(root);

        // Queries all the buttons (via type) in our root and passes them
        // in the SetupButton method.
        var toolButtons = root.Query<Button>();
        toolButtons.ForEach(SetupButton);
    }

    private void SetupButton(Button button)
    {
        // Reference to the VisualElement inside the button that serves
        // as the button¡¯s icon.
        var buttonIcon = button.Q(className: "quicktool-button-icon");

        // Icon¡¯s path in our project.
        var iconPath = "Assets/Editor/QuickTools/Icons/" + button.parent.name + "_icon.png";

        // Loads the actual asset from the above path.
        var iconAsset = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);

        // Applies the above asset as a background image for the icon.
        buttonIcon.style.backgroundImage = iconAsset;

        // Instantiates our primitive object on a left click.
        button.clickable.clicked += () => CreateObject(button.parent.name);

        // Sets a basic tooltip to the button itself.
        button.tooltip = button.parent.name;
    }

    private void CreateObject(string primitiveTypeName)
    {
        var pt = (PrimitiveType)Enum.Parse
                     (typeof(PrimitiveType), primitiveTypeName, true);
        var go = ObjectFactory.CreatePrimitive(pt);
        go.transform.position = Vector3.zero;
    }
}