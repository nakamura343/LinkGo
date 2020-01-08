using UnityEngine;
using System;
using System.Collections.Generic;

namespace ScriptTemplateEditor
{
    [CreateAssetMenu(menuName = "ScriptTemplateSetting")]
    public class ScriptTemplateSetting : ScriptableObject
    {
        [SerializeField]
        public string TemplatePath;

        [SerializeField]
        public List<TemplateMacro> Macros;
    }

    [Serializable]
    public class TemplateMacro
    {
        [SerializeField]
        public string key;

        [SerializeField]
        public string value;
    }
}

