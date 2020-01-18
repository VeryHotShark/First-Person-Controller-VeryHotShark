using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class StraightProjectile : Projectile
    {
        private ProjectileStraightData m_projectileStraightData;

        protected override void CastData() => m_projectileStraightData = projectileData as ProjectileStraightData;

        public override void OnUpdate(float _deltaTime)
        {
            base.OnUpdate(_deltaTime);
            transform.Translate(transform.forward * _deltaTime * m_projectileStraightData.SpecificSettings.Speed,Space.World);
        }
    }
}
