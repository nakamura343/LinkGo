using UnityEngine;

namespace Mongo.Common.I18N
{
    [ExecuteInEditMode]
    public class I18NBehaviour : MonoBehaviour
    {
        public static System.Action OnLocalizationChanged;

        private string _key;
        public string key
        {
            get
            {
                return _key;
            }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    UpdateTranslations();
                }
            }
        }

        private LanguageCode _currentLanguage;
        public LanguageCode curentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    UpdateTranslations();
                }
            }
        }

#if UNITY_EDITOR
        // Inspector modified?
        protected virtual void OnValidate()
        {
            UpdateTranslations();
        }
#endif

        public static void UpdateTranslations()
        {

            // Notify changes?
            if (OnLocalizationChanged != null)
            {
                OnLocalizationChanged();
            }
        }

    }
}


