using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public enum WeaponTriggerType
    {
        PullRelease,
        Continous
    }


    [CreateAssetMenu(fileName = "weaponData", menuName = "WeaponSystem/weaponData")]
    public class WeaponData : ScriptableObject
    {
        [Space, Header("Weapon Data")]

        [SerializeField] private WeaponTriggerType triggerType;
        [SerializeField, Tooltip("-1 = infinite")] private int ammoCount = 0;
        [SerializeField] private int roundsPerSecond = 0;
        [SerializeField] private float reloadDuration = 0f;


        public WeaponTriggerType TriggerType => triggerType;
        public int AmmoCount => ammoCount;
        public int RoundsPerSecond => roundsPerSecond;
        public float ReloadDuration => reloadDuration;
        public float TimeBetweenRounds => m_timeBetweenRounds;

        private float m_timeBetweenRounds;

        public void Init() => m_timeBetweenRounds = 1f / roundsPerSecond;
    }
}
