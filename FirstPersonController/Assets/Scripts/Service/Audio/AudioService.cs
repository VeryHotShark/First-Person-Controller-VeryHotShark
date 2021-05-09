using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VHS.Audio
{
    public class AudioService
    {
        Dictionary<string, AudioClip> _seAudioClips = new Dictionary<string, AudioClip>();
        Dictionary<string, AudioClip> _bgmAudioClips = new Dictionary<string, AudioClip>();

        AudioSource _bgmAudioSource;

        public async UniTask Initialize()
        {
            await Preload();

            _bgmAudioSource = Camera.main.transform.GetComponent<AudioSource>();
        }

        async UniTask Preload()
        {
            var master = await Addressables.LoadAssetAsync<AudioMaster>(AudioMaster.FilePath);
            foreach (var audioClip in master._seAudioClips)
            {
                _seAudioClips.Add(audioClip.name, audioClip);
            }
            foreach (var audioClip in master._bgmAudioClips)
            {
                _bgmAudioClips.Add(audioClip.name, audioClip);
            }
        }

        public void PlayBGM(string key)
        {
            if (!_bgmAudioClips.ContainsKey(key)) return;
            _bgmAudioSource.clip = _bgmAudioClips[key];
            _bgmAudioSource.loop = true;
            _bgmAudioSource.Play();
        }

        public void PlaySe(string key, AudioSource audioSource)
        {
            if (!_seAudioClips.ContainsKey(key)) return;
            audioSource.PlayOneShot(_seAudioClips[key]);
        }
    }
}
