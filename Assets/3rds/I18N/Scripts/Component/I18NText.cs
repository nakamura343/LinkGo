using UnityEngine;
using UnityEngine.UI;

namespace Mongo.Common.I18N
{
    [RequireComponent(typeof(Text))]
    [DisallowMultipleComponent]
    public class I18NText : I18NBehaviour
    {
        private Text m_textInst;

        private void Awake()
        {
            m_textInst = GetComponent<Text>();
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void OnEnable()
        {
            string text = I18N.instance.GetText("dkek");
            m_textInst.text = text;
        }
    }
}