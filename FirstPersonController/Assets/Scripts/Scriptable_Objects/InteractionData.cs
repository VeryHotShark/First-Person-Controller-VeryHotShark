using UnityEngine;

namespace VHS
{    
    [CreateAssetMenu(fileName = "InteractionData", menuName = "FirstPersonController/Data/InteractionData", order = 0)]
    public class InteractionData : ScriptableObject
    {
        private InteractableBase interactable;

        public InteractableBase Interactable
        {
            get => interactable;
            set => interactable = value;
        }

        public bool IsEmpty()
        {
            if(interactable != null)
                return false;
            else
                return true;
        }

        public bool IsSameInteractable(InteractableBase _interactable)
        {
            if(interactable == _interactable)
                return true;
            else
                return false;
        }

        public void Interact()
        {
            interactable.OnInteract();
            interactable = null;
        }

        public void ResetData()
        {
            interactable = null;
        }
    }
}