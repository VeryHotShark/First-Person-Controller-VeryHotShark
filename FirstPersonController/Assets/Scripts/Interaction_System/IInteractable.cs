using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public interface IInteractable
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        bool Multiple { get; }
<<<<<<< HEAD
=======
        string Tooltip { get; set;}
        Transform TooltipTransform { get; }
>>>>>>> parent of 391627a... Revert "InteractionSystem"

        void OnInteract();
        bool IsInteractable();

    }
}
