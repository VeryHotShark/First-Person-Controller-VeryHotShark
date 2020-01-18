using UnityEngine;

namespace VHS
{
    [CreateAssetMenu(fileName = "ProjectileStraightData", menuName = "WeaponSystem/Projectiles/Straight")]
    public class ProjectileStraightData : ProjectileData
    {
        [SerializeField] protected ProjectileStraightSettings specificSettings = new ProjectileStraightSettings();
        public ProjectileStraightSettings SpecificSettings => specificSettings;
        
        [System.Serializable]
        public struct ProjectileStraightSettings 
        {
            [SerializeField] private float speed;
            public float Speed => speed;
        }
    }
}