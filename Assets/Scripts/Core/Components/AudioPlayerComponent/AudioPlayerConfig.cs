using System;
using System.Collections.Generic;
using UnityEngine;
using Wooff.ECS;

namespace Core.Components.AudioPlayerComponent
{
    [Serializable]
    public class AudioPlayerConfig : IConfig
    {
        public List<AudioItem> StringAudioClipDictionary;
    }
}