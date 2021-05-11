using UnityEngine;
using VContainer;
using VHS.Audio;

namespace VHS
{
    public class FirstPersonSoundEffectController : MonoBehaviour
    {
        [SerializeField] FootstepSoundEffectData _footstepSoundEffectData;
        [SerializeField] AudioSource _footStepAudioSrouce;

        FootstepSoundEffect _footstepSoundEffect;
        public FootstepSoundEffect FootstepSoundEffect => _footstepSoundEffect;

        [Inject]
        public void Inject(AudioService audioService)
        {
            _footstepSoundEffect = new FootstepSoundEffect(audioService, _footstepSoundEffectData, _footStepAudioSrouce);
        }
    }

}
