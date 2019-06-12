using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public interface IHoverable
    {
        string Tooltip { get; set;}
        Transform TooltipTransform { get; }

        void OnHoverStart(Material _hoverMat);
        void OnHoverEnd();
    }
}
