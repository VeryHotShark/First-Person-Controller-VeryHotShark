using UnityEngine;

namespace VHS
{
    [CreateAssetMenu(fileName = "ProjectileMissileData", menuName = "WeaponSystem/Projectiles/Missile")]
    public class ProjectileMissileData : ProjectileData
    {
        [SerializeField] private ProjectileMissileSettings specificSettings = new ProjectileMissileSettings();
        public ProjectileMissileSettings SpecificSettings => specificSettings;

        [System.Serializable]
        public struct ProjectileMissileSettings 
        {
            [SerializeField] private float followDuration;
            public float FollowDuration => followDuration;
        }
    }
}