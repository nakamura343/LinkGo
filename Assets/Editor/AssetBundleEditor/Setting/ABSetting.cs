using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ABBuildEditor
{
    [CreateAssetMenu(fileName ="ABSetting", menuName ="ABSetting",order=0)]
    public class ABSetting : ScriptableObject
    {
        [SerializeField]
        public string BundleOutputPath; //Bundle输出目录

        [SerializeField]
        public List<PackABFilter> filters;

        [SerializeField]
        public List<PackABRule> rules;
    }

    [Serializable]
    public class PackABRule
    {
        [SerializeField]
        public string path;

        [SerializeField]
        public List<string> filterKeys;

    }

    [Serializable]
    public class PackABFilter
    {
        [SerializeField]
        public string key;
    }
}


