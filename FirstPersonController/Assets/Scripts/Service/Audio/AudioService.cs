using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VHS.Audio
{
    public class AudioService
    {
        Dictionary<string, AudioClip> _audioDatas = new Dictionary<string, AudioClip>();

        public async UniTask Initialize()
        {
            await Preload();
        }

        async UniTask Preload()
        {
            var master = await Addressables.LoadAssetAsync<AudioMaster>(AudioMaster.FilePath);
            foreach (var audioClip in master._seAudioClips)
            {
                _audioDatas.Add(audioClip.name, audioClip);
            }
        }

        public void PlaySe(string key, AudioSource audioSource)
        {
            if (!_audioDatas.ContainsKey(key)) return;
            audioSource.PlayOneShot(_audioDatas[key]);
        }
    }
}
