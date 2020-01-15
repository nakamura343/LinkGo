using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using ShaderVariant = UnityEngine.ShaderVariantCollection.ShaderVariant;

namespace ShaderVariantEditor
{
    class ShaderVariantStripBuildProcessor : IPreprocessShaders
    {
        private static ShaderVariantCollection keepCustomVariants;
        private static ShaderVariantCollection keepExportVariants;
        public ShaderVariantStripBuildProcessor()
        {
            string filePath = "Assets/ResourcesAb/shader/";
            string custiomSVCFile = filePath + "CustomShaderVariants.shadervariants";
            string exportSVCFile = filePath + "allshader.shadervariants";
            //ShaderVariantsCollection.Collect(exportSVCFile);
            keepCustomVariants = AssetDatabase.LoadAssetAtPath<ShaderVariantCollection>(custiomSVCFile);
            keepExportVariants = AssetDatabase.LoadAssetAtPath<ShaderVariantCollection>(exportSVCFile);
        }

        public int callbackOrder { get { return 0; } }

        public void OnProcessShader(
            Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> shaderCompilerData)
        {
            Debug.Log("OnProcessShader->" + shader.name);

            for (int i = 0; i < shaderCompilerData.Count; ++i)
            {
                if (!KeepVariants(shader, snippet, shaderCompilerData[i]))
                {
                    Debug.LogWarning(shader.name + "\tStrip variants:" + string.Join("\t", keywords.ToArray()));
                    shaderCompilerData.RemoveAt(i);
                    --i;
                }
            }
        }

        private List<string> keywords = new List<string>();
        public bool KeepVariants(Shader shader, ShaderSnippetData snippet, ShaderCompilerData variants)
        {
            keywords.Clear();
            var shaderKeywords = variants.shaderKeywordSet.GetShaderKeywords();
            foreach (var shaderkeyword in shaderKeywords)
            {
                keywords.Add(ShaderKeyword.GetGlobalKeywordName(shaderkeyword));
            }
            bool hasKeywords = keywords.Count > 0;
            Debug.Log(shader.name + "\tVariants:" + (hasKeywords ? string.Join("\t", keywords.ToArray()) : "<no keywords>"));

            if (hasKeywords && keepExportVariants != null)
            {
                ShaderVariant shaderVariant = new ShaderVariant(shader, snippet.passType, keywords.ToArray());
                // 必须，自动导出的变体组合
                if (keepExportVariants.Contains(shaderVariant))
                {
                    return true;
                }
                // 可选，自定义变体组合
                if (keepCustomVariants != null && keepCustomVariants.Contains(shaderVariant))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}

