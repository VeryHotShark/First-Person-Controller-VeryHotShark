using UnityEngine;

namespace VHS
{
    [CreateAssetMenu(fileName = "PickableData", menuName = "FirstPersonController/Data/PickableData", order = 0)]
    public class PickableData : ScriptableObject
    {
        private Pickable pickable;
        public Pickable PickableItem
        {
            get => pickable;
            set => pickable = value;
        }

        public bool IsEmpty()
        {
            if (pickable != null)
                return false;
            else
                return true;
        }

        public bool IsSamePickable(Pickable _pickable)
        {
            if(pickable == _pickable)
                return true;
            else
                return false;
        }

        public void Pick()
        {
            pickable.OnPickUp();
        }

        public void Hold()
        {
            pickable.OnHold();
        }

        public void Release()
        {
            pickable.OnRelease();
        }

        public void ResetData()
        {
            pickable = null;
        }

    }
}