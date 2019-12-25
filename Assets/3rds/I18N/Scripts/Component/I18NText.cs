using UnityEngine;
using UnityEngine.UI;

namespace Mongo.Common.I18N
{
    [RequireComponent(typeof(Text))]
    [DisallowMultipleComponent]
    public class I18NText : I18NBehaviour
    {
        private Text _text;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            string text = I18N.instance.GetText("dkek");
            _text.text = text;
        }

    }
}