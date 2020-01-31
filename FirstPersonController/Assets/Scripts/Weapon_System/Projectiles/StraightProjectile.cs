using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class StraightProjectile : Projectile
    {
        private ProjectileStraightData m_projectileStraightData;

        protected override void CastData() => m_projectileStraightData = projectileData as ProjectileStraightData;
        protected override void UpdatePosition(float _deltaTime)
        {
            transform.position += m_currentDirection * m_projectileStraightData.SpecificSettings.Speed * _deltaTime;
            base.UpdatePosition(_deltaTime);
        }
    }
}
