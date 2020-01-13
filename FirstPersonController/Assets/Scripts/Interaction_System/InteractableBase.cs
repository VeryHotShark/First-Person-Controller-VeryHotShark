using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Variables    
            [Header("Interactable Settings")]
            public float holdDuration;

            [Space]
            public bool holdInteract;
            public bool multipleUse;
            public bool isInteractable;

            public float HoldDuration => holdDuration;
        #endregion

        #region Properties    
            public bool HoldInteract => holdInteract;
            public bool MultipleUse => multipleUse;
            public bool IsInteractable => isInteractable;
        #endregion

        #region Methods
            public virtual void OnInteract()
            {
                Debug.Log("INTERACTED: " + gameObject.name);
            }
        #endregion
    }
}
