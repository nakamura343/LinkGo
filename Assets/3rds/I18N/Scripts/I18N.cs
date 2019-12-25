using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Mongo.Common.I18N
{
    /// <summary>
    /// I18N Language code enum.
    /// </summary>
    public enum LanguageCode
    {
        zh_CN = 0,
        en_US = 1,
        Count
    }

    public sealed class I18N
    {
        private static I18N _instance;
        public static I18N instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new I18N();
                }
                return _instance;
            }
        }

        private LanguageCode _languageCode;

        private I18N()
        {
            _languageCode = LanguageCode.zh_CN;
        }

        public void SetLanguageCode(LanguageCode code)
        {
            if (_languageCode != code)
            {
                _languageCode = code;
            }
        }

        public string GetText(string key)
        {
            return GetText(key, _languageCode);
        }

        public string GetText(string key, LanguageCode code)
        {
            return "";
        }

        public string GetImage(string key)
        {
            return GetImage(key, _languageCode);
        }

        public string GetImage(string key, LanguageCode code)
        {
            return "";
        }

        public int GetAudio(string key)
        {
            return GetAudio(key, _languageCode);
        }

        public int GetAudio(string key, LanguageCode code)
        {
            return 0;
        }
    }
}