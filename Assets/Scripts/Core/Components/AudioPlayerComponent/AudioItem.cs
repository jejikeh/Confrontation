using System;
using UnityEngine;

namespace Core.Components.AudioPlayerComponent
{
    [Serializable]
    public class AudioItem
    {
        public string Name;
        public AudioClip Clip;
    }
}