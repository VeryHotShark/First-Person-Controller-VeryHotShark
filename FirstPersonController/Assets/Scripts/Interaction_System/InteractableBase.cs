using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace VHS
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
<<<<<<< HEAD
=======
        [BoxGroup("Settings")] public string tooltip;
        [BoxGroup("Settings")] public Transform tooltipTransform;
>>>>>>> parent of 391627a... Revert "InteractionSystem"
        [BoxGroup("Settings")] public bool multiple;
        [BoxGroup("Settings")] public bool holdInteract;
        [BoxGroup("Settings")] [ShowIf("holdInteract")] public float holdDuration;

        protected bool m_interacted;
        protected bool m_interactable = true;

        public float HoldDuration => holdDuration;
        public bool HoldInteract => holdInteract;
        public bool Multiple => multiple;
<<<<<<< HEAD
=======
        public string Tooltip
        {
            get => tooltip;
            set => tooltip = value;
        }

        public Transform TooltipTransform => tooltipTransform;
 
>>>>>>> parent of 391627a... Revert "InteractionSystem"

        public virtual void OnInteract()
        {
            m_interacted = true;
        }

        public virtual bool IsInteractable()
        {
            if(multiple)
            {
                if(m_interactable)
                    return true;

                return false;
            }
            else
            {
                if(m_interacted)
                    return false;

                return true;
            }
        }
    }
}
