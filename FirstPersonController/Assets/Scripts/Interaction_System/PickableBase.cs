using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public abstract class PickableBase : MonoBehaviour, IPickable
    {

        private bool Picked
        {
            get;
            set;
        }

        public void OnHold()
        {

        }

        public void OnPickUp()
        {

        }

        public void OnRelease()
        {

        }
    }
}
