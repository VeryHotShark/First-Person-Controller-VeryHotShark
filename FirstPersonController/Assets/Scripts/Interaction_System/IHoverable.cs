using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public interface IHoverable
    {
        void OnHoverStart(Material _hoverMat);
        void OnHoverEnd();
    }
}
