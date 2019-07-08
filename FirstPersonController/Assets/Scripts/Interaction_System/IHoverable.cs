using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public interface IHoverable
    {
<<<<<<< HEAD
        string Tooltip { get; set;}
        Transform TooltipTransform { get; }

=======
>>>>>>> parent of 391627a... Revert "InteractionSystem"
        void OnHoverStart(Material _hoverMat);
        void OnHoverEnd();
    }
}
