using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class HitScanProjectile : Projectile
    {
        private ProjectileHitScanData m_projectileHitScanData;
        protected override void CastData() => m_projectileHitScanData = projectileData as ProjectileHitScanData;

        public override bool IsPoolable => true;

        protected override void OnEnable() {}
        protected override void OnDisable() {}

        public override void OnFire(Transform _owner)
        {
            base.OnFire(_owner);
            CheckForCollision();
        }

        public override void CheckForCollision()
        {
            bool _hitSomething = Physics.Linecast(transform.position, transform.position + transform.forward * m_projectileHitScanData.SpecificSettings.ScanDistance, out m_hitInfo, collisionLayers);

            if (_hitSomething)
            {
                if (m_hitInfo.collider.transform.IsChildOf(transform) || m_hitInfo.collider.transform.IsChildOf(m_owner))
                    return;

                OnHit();
            }
        }

        public override void OnHit() => Debug.Log(m_hitInfo.collider.name);
    }
}
