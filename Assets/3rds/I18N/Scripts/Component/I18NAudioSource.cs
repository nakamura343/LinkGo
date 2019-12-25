using UnityEngine;

namespace Mongo.Common.I18N
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class I18NAudio : I18NBehaviour
    {
        private AudioSource _audioSource;
    }
}