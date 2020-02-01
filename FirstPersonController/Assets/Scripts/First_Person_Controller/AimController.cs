using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class AimController : PlayerComponent<AimController>
    {
        [SerializeField] private float maxRayDistance = 1000f;
        [SerializeField] private LayerMask collisionLayer = ~0;

    }
}
