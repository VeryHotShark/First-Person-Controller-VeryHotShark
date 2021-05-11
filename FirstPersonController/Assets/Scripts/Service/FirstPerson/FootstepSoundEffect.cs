using UnityEngine;
using VHS.Audio;

namespace VHS
{
    public class FootstepSoundEffect
    {
        ShuffleArray<string> _defaultFootstepSeKey;
        AudioService _audioService;
        AudioSource _audioSource;
        FootstepSoundEffectData _data;

        public FootstepSoundEffect(AudioService audioService, FootstepSoundEffectData data, AudioSource audioSource = default)
        {
            _audioService = audioService;
            _data = data;
            _audioSource = audioSource;

            _defaultFootstepSeKey = new ShuffleArray<string>(data.NameArray);
        }

        public void PlaySe(float normalizationSpeed)
        {
            var volumeScale = _data._volumeCurve.Evaluate(Mathf.Clamp01(normalizationSpeed));
            _audioService.PlaySe(_defaultFootstepSeKey.Get(), volumeScale, _audioSource);
        }
    }
}
