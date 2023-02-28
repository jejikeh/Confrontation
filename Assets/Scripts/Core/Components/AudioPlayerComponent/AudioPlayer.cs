using System.Linq;
using UnityEngine;
using Wooff.ECS;
using Wooff.ECS.Components;
using Wooff.MonoIntegration;

namespace Core.Components.AudioPlayerComponent
{
    public class AudioPlayer : Component<AudioPlayerConfig, IMonoEntity>, IComponent<IConfig, IMonoEntity>
    {
        IConfig IConfigurable<IConfig>.Config => Config;
        private AudioSource _audioSource;
        
        
        public AudioPlayer(AudioPlayerConfig data, IMonoEntity handler) : base(data, handler)
        {
            _audioSource = Handler.MonoObject.AddComponent<AudioSource>();
        }

        public void Play(string name)
        {
            _audioSource.clip = Config.StringAudioClipDictionary.FirstOrDefault(x => x.Name == name)?.Clip;
            _audioSource.Play();
        }
    }
}