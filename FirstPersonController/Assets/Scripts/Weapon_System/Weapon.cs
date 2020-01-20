using System;
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

    public class Weapon : MonoBehaviour
    {

        [SerializeField] private WeaponData weaponData = null;
        [SerializeField] private Projectile projectile = null;

        public WeaponData Data => weaponData;

        protected bool CanShootWeapon
        {
            get
            {
                if(m_timeSinceLastShot + weaponData.TimeBetweenRounds < Time.time && m_currentAmmoCount > 0)
                    return true;

                return false;
            }
        }

        protected int m_currentAmmoCount;
        private float m_timeSinceLastShot;
        
        private WeaponProjectileAnchor m_projectileAnchor;

        public virtual void Awake()
        {
            weaponData.Init();
            m_currentAmmoCount = weaponData.AmmoCount;
            m_projectileAnchor = GetComponentInChildren<WeaponProjectileAnchor>();
        }

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnWeaponShootAttempt()
        {
            if(!CanShootWeapon) return;

            OnWeaponShot();
        }

        private void OnWeaponShot()
        {
            m_currentAmmoCount--;
            m_timeSinceLastShot = Time.time;

            IPoolable _poolableProjectile = PoolManager.SpawnPoolable(projectile,m_projectileAnchor.transform.position, m_projectileAnchor.transform.rotation, projectile.transform.localScale);
            Projectile _projectileInstance = _poolableProjectile.Transform.gameObject.GetComponent<Projectile>();
            _projectileInstance.OnFire(transform.root);
        }

        public virtual void OnReloadWeaponPressed()
        {
            m_currentAmmoCount = weaponData.AmmoCount;
        }
    }
}
