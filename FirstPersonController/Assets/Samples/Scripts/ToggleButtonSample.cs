using UnityEngine;
using VContainer;
using VHS;
using VHS.Audio;

namespace Sample
{
    public class ToggleButtonSample : MonoBehaviour
    {
        [SerializeField] MeshRenderer _mesh;
        [SerializeField] Collider _collider;
        [SerializeField] AudioSource _audioSource;

        AudioService _audioService;

        [Inject]
        public void Inject(AudioService audioService)
        {
            _audioService = audioService;
        }

        public void OnToggleButtonSelected(ToggleButtonInteractable toggleButtonInteractable)
        {
            _audioService.PlaySe("se_click", _audioSource);

            _mesh.enabled = toggleButtonInteractable.IsOn;
            _collider.enabled = toggleButtonInteractable.IsOn;
        }

        private void Reset()
        {
            _mesh = transform.GetComponent<MeshRenderer>();
            _collider = transform.GetComponent<Collider>();
            _audioSource = transform.GetComponent<AudioSource>();
        }
    }
}
