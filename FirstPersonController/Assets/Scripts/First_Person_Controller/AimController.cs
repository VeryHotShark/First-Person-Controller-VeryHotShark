using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class AimController : PlayerComponent
    {
        [SerializeField] private float maxRayDistance = 1000f;
        [SerializeField] private LayerMask collisionLayer = ~0;

        public Vector3 AimPoint { get; private set; }

        private void OnEnable()
        {
            UpdateManager._OnUpdate += _OnUpdate;
        }

        private void OnDisable()
        {
            UpdateManager._OnUpdate -= _OnUpdate;
        }

        private void _OnUpdate(float _dt)
        {
            HandleCurrentAimPoint();
        }

        private void HandleCurrentAimPoint()
        {
            bool _hitSomething = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxRayDistance, collisionLayer);
            AimPoint = _hitSomething ? hitInfo.point : transform.forward * maxRayDistance;
        }
    }
}
