using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    [CreateAssetMenu(fileName = "weaponData", menuName = "WeaponSystem/weaponData")]
    public class WeaponData : ScriptableObject
    {
        [Space, Header("Weapon Data")]

        [SerializeField, Tooltip("-1 = infinite")] private int ammoCount = 0;
        [SerializeField] private int roundsPerSecond = 0;
        [SerializeField] private float reloadDuration = 0f;

        public int AmmoCount => ammoCount;
        public int RoundsPerSecond => roundsPerSecond;
        public float ReloadDuration => reloadDuration;
        public float TimeBetweenRounds => m_timeBetweenRounds;

        private float m_timeBetweenRounds;

        public void Init() => m_timeBetweenRounds = 1f / roundsPerSecond;
    }
}
