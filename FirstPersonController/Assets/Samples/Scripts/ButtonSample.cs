using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Sample
{
    public class ButtonSample : MonoBehaviour
    {
        [SerializeField] MeshRenderer _mesh;
        [SerializeField] Collider _collider;

        CancellationTokenSource _cts = default;

        public void OnButtonSelected()
        {
            _cts?.Cancel();

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
        }
    }
}
