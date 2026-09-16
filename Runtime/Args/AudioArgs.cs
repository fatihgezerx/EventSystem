using UnityEngine;

namespace EventSystem
{
    /// <summary>Event data carrying a single <see cref="AudioClip"/> reference.</summary>
    public sealed class AudioArgs
    {
        public AudioClip Value;

        public AudioArgs(AudioClip value)
        {
            Value = value;
        }
    }
}
