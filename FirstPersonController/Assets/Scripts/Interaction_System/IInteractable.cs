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
        string Tooltip { get; set;}
        Transform TooltipTransform { get; }

        void OnInteract();
        bool IsInteractable();

    }
}
