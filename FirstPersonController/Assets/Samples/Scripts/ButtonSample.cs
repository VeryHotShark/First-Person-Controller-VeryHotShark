using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using VContainer;
using VHS.Audio;

namespace Sample
{
    public class ButtonSample : MonoBehaviour
    {
        [SerializeField] MeshRenderer _mesh;
        [SerializeField] Collider _collider;
        [SerializeField] AudioSource _audioSource;

        CancellationTokenSource _cts = default;

        AudioService _audioService;

        [Inject]
        public void Inject(AudioService audioService)
        {
            _audioService = audioService;
        }

        public void OnButtonSelected()
        {

            _cts?.Cancel();

            _audioService?.PlaySe("se_click", _audioSource);

            UniTask.Void(async () =>
            {
                _cts = new CancellationTokenSource();
                var ct = _cts.Token;

                try
                {
                    ct.ThrowIfCancellationRequested();
                    _mesh.enabled = false;
                    _collider.enabled = false;
                    await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);
                    ct.ThrowIfCancellationRequested();
                    _mesh.enabled = true;
                    _collider.enabled = true;
                }
                finally
                {
                    _cts = default;
                }
            });
        }

        private void Reset()
        {
            _mesh = transform.GetComponent<MeshRenderer>();
            _collider = transform.GetComponent<Collider>();
            _audioSource = transform.GetComponent<AudioSource>();
        }
    }
}
