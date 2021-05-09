using UnityEngine;
using VContainer;
using VHS.Audio;

namespace Sample
{
    public class SampleScenePresenter : MonoBehaviour
    {
        AudioService _audioService;

        [Inject]
        public void Inject(AudioService audioService)
        {
            _audioService = audioService;
        }

        // Start is called before the first frame update
        async void Start()
        {
            await _audioService.Initialize();

            _audioService.PlayBGM("bgm");
        }
    }
}
