using UnityEngine;
using VHS;

namespace Sample
{
    public class ToggleButtonSample : MonoBehaviour
    {
        [SerializeField] MeshRenderer _mesh;
        [SerializeField] Collider _collider;

        public void OnToggleButtonSelected(ToggleButtonInteractable toggleButtonInteractable)
        {
            _mesh.enabled = toggleButtonInteractable.IsOn;
            _collider.enabled = toggleButtonInteractable.IsOn;
        }

        private void Reset()
        {
            _mesh = transform.GetComponent<MeshRenderer>();
            _collider = transform.GetComponent<Collider>();
        }
    }
}
