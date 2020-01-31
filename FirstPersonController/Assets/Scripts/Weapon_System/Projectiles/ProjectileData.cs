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

    public abstract class ProjectileData : ScriptableObject
    {
        [Space, Header("Projectile Data")]

        [SerializeField] private ProjectileGeneralSettings generalSettings = new ProjectileGeneralSettings();
        public ProjectileGeneralSettings GeneralSettings => generalSettings;

        [System.Serializable]
        public struct ProjectileGeneralSettings
        {
            [SerializeField] private ProjectileType projectileType;
            [SerializeField] private float damage;
            [SerializeField] private float maxLiveDuration;

            public ProjectileType ProjectileType => projectileType;
            public float Damage => damage;
            public float MaxLiveDuration => maxLiveDuration;
        }
    }
}
