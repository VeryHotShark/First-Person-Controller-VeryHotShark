using UnityEngine;
using UnityEngine.Events;

namespace VHS
{
    public class ButtonInteractable : InteractableBase
    {
        [SerializeField] UnityEvent _unityEvent = default;

        public override void OnInteract()
        {
            base.OnInteract();

            _unityEvent?.Invoke();
        }
    }
}
