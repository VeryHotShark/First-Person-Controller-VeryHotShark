using UnityEngine;

namespace VHS
{
    [CreateAssetMenu(fileName = "ProjectileHitScanData", menuName = "WeaponSystem/Projectiles/HitScan")]
    public class ProjectileHitScanData : ProjectileData
    {
        [SerializeField] private ProjectileHitScanSettings specificSettings = new ProjectileHitScanSettings();
        public ProjectileHitScanSettings SpecificSettings => specificSettings;
        
        [System.Serializable]
        public struct ProjectileHitScanSettings 
        {
            [SerializeField] private float scanDistance;
            public float ScanDistance => scanDistance;
        }
    }
}