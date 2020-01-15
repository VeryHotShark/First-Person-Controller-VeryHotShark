using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace VHS
{
    public enum ProjectileType
    {
        Straight,
        HitScan,
        Missile,
        Arc
    }

    [CreateAssetMenu(fileName = "projectileData", menuName = "WeaponSystem/projectileData")]
    public class ProjectileData : ScriptableObject
    {
        [Space, Header("Projectile Data")]

        [SerializeField] private ProjectileType projectileType = ProjectileType.Straight;

        public bool ProjectileStraight () => projectileType == ProjectileType.Straight;
        public bool ProjectileArc () => projectileType == ProjectileType.Arc;
        public bool ProjectileMissile () => projectileType == ProjectileType.Missile;
        public bool ProjectileHitScan () => projectileType == ProjectileType.HitScan;

        [Space][SerializeField, ShowIf("ProjectileStraight")] private ProjectileStraightSettings projectileStraightSettings = new ProjectileStraightSettings();
        [Space][SerializeField, ShowIf("ProjectileMissile")] private ProjectileMissileSettings projectileMissileSettings = new ProjectileMissileSettings();
        [Space][SerializeField, ShowIf("ProjectileHitScan")] private ProjectileHitScanSettings projectileHitScanSettings = new ProjectileHitScanSettings();
        [Space][SerializeField, ShowIf("ProjectileArc")] private ProjectileArcSettings projectileArcSettings = new ProjectileArcSettings();
        
        private ProjectileSettings projectileSettings = new ProjectileSettings();
        public ProjectileSettings ProjectileSettings
        { 
            get => projectileSettings;
            set => projectileSettings = value;
        }


        public void InitProjectileSettings()
        {
            switch(projectileType)
            {
                case ProjectileType.Straight:
                    ProjectileSettings = projectileStraightSettings;
                    break;
                case ProjectileType.HitScan:
                    ProjectileSettings = projectileHitScanSettings;
                    break;
                case ProjectileType.Missile:
                    ProjectileSettings = projectileMissileSettings;
                    break;
                case ProjectileType.Arc:
                    ProjectileSettings = projectileArcSettings;
                    break;
            }
        }
    }

    [System.Serializable]
    public class ProjectileSettings
    {
        [SerializeField] private float projectileDamage;
    }

    [System.Serializable]
    public class ProjectileStraightSettings : ProjectileSettings
    {
        [SerializeField] private float projectileSpeed = 0;

        public float ProjectileSpeed => projectileSpeed;
    }

    [System.Serializable]
    public class ProjectileArcSettings : ProjectileSettings
    {
        [SerializeField] private float projectilePower;
    }

    [System.Serializable]
    public class ProjectileHitScanSettings : ProjectileSettings
    {
        [SerializeField] private float hitScanRange;
    }

    [System.Serializable]
    public class ProjectileMissileSettings : ProjectileSettings
    {
        [SerializeField] private float missileFollowSpeed;
    }
}
