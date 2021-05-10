using UnityEngine;
using VHS.Audio;

namespace VHS
{
    public class FootstepSoundEffect
    {
        ShuffleSoundEffect _shuffleSoundEffect;

        public FootstepSoundEffect(AudioService audioService, FootstepSoundEffectData data, AudioSource audioSource = default)
        {
            _shuffleSoundEffect = new ShuffleSoundEffect(audioService, data.NameArray, audioSource);
        }

        public void PlaySe()
        {
            _shuffleSoundEffect.PlaySe();
        }
    }
}
