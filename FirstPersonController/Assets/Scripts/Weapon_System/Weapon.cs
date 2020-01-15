using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData = null;
        [SerializeField] private Projectile projectile = null;

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

        public virtual void OnShootWeaponPressed()
        {
            if(!CanShootWeapon) return;

            OnWeaponShot();
        }

        private void OnWeaponShot()
        {
            m_currentAmmoCount--;
            m_timeSinceLastShot = Time.time;

            Projectile projectileInstance =  Instantiate(projectile,m_projectileAnchor.transform.position, m_projectileAnchor.transform.rotation) as Projectile;
            projectileInstance.OnFire();
        }

        public virtual void OnReloadWeaponPressed()
        {
            m_currentAmmoCount = weaponData.AmmoCount;
        }
    }
}
