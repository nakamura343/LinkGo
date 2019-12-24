using UnityEngine;
using UnityEngine.UI;

namespace Mongo.Common.I18N
{
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public class I18NImage : I18NBehaviour
    {
        private Image _img;
    }
}