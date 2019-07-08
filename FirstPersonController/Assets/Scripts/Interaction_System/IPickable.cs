using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public interface IPickable
    {
<<<<<<< HEAD
        Rigidbody Rigid {get;set;}

=======
>>>>>>> parent of 391627a... Revert "InteractionSystem"
        void OnPickUp();
        void OnHold();
        void OnRelease();
    }
}
