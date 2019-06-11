using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public interface IPickable
    {
        void OnPickUp();
        void OnHold();
        void OnRelease();
    }
}
