using UnityEngine;

namespace VHS
{
    [CreateAssetMenu(fileName = "ProjectileArcData", menuName = "WeaponSystem/Projectiles/Arc")]
    public class ProjectileArcData : ProjectileData
    {
        [SerializeField] private ProjectileArcSettings specificSettings = new ProjectileArcSettings();
        public ProjectileArcSettings SpecificSettings => specificSettings;
        
        [System.Serializable]
        public struct ProjectileArcSettings
        {
            [SerializeField] private float angle;

            public float Angle => angle;
        }
    }
}