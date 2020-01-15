using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class StraightProjectile : Projectile
    {
        private ProjectileStraightSettings m_projectileSettings = new ProjectileStraightSettings();

        protected override void Awake()
        {
            base.Awake();
            m_projectileSettings = projectileData.ProjectileSettings as ProjectileStraightSettings;
        }

        public override void OnUpdate(float _deltaTime)
        {
            transform.Translate(transform.forward * _deltaTime * m_projectileSettings.ProjectileSpeed,Space.World);
        }

        public override void OnFire()
        {
            Debug.Log("I WAS FIRED");
        }
    }
}
