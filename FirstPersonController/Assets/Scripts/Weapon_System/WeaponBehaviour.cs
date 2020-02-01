using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class WeaponBehaviour : WeaponComponent<WeaponBehaviour>
    {
        protected bool CanShootWeapon
        {
            get
            {
                if(Weapon.DuringReload)
                    return false;

                if (Weapon.TimeSinceLastShot + Weapon.Data.TimeBetweenRounds < Time.time && Weapon.CurrentAmmoCount > 0)
                    return true;

                return false;
            }
        }


        private void OnEnable()
        {
            Weapon.OnWeaponReloadStarted += OnWeaponReloadStarted;
            Weapon.OnWeaponReloadCompleted += OnWeaponReloadCompleted;

            Weapon.OnWeaponShootReleased += OnWeaponShootReleased;

            switch (Weapon.Data.TriggerType)
            {
                case WeaponTriggerType.PullRelease:
                    Weapon.OnWeaponShootPressed += OnWeaponShootPressed;
                    break;
                case WeaponTriggerType.Continous:
                    Weapon.OnWeaponShootHeld += OnWeaponShootHeld;
                    break;
            }
        }

        private void OnDisable()
        {
            Weapon.OnWeaponReloadStarted -= OnWeaponReloadStarted;
            Weapon.OnWeaponReloadCompleted -= OnWeaponReloadCompleted;

            Weapon.OnWeaponShootReleased -= OnWeaponShootReleased;

            switch (Weapon.Data.TriggerType)
            {
                case WeaponTriggerType.PullRelease:
                    Weapon.OnWeaponShootPressed -= OnWeaponShootPressed;
                    break;
                case WeaponTriggerType.Continous:
                    Weapon.OnWeaponShootHeld -= OnWeaponShootHeld;
                    break;
            }
        }

        private void OnWeaponShootHeld()
        {
            if (!CanShootWeapon) return;
            OnWeaponShot();
        }

        private void OnWeaponShootPressed()
        {
            if (!CanShootWeapon) return;
            OnWeaponShot();
        }
        
        private void OnWeaponShootReleased() { }

        private void OnWeaponShot()
        {
            Weapon.OnWeaponShootSucceed();

            Weapon.CurrentAmmoCount--;
            Weapon.TimeSinceLastShot = Time.time;

            IPoolable _poolableProjectile = PoolManager.SpawnPoolable(Weapon.Projectile, Weapon.ProjectileAnchor.transform.position, Weapon.ProjectileAnchor.transform.rotation, Weapon.Projectile.transform.localScale);
            Projectile _projectileInstance = _poolableProjectile.Transform.gameObject.GetComponent<Projectile>();
            _projectileInstance.OnFire(transform.root);
        }

        public virtual void OnWeaponReloadStarted()
        {
            Weapon.DuringReload = true;
        }

        public virtual void OnWeaponReloadCompleted()
        {
            Weapon.DuringReload = false;
            Weapon.CurrentAmmoCount = Weapon.Data.AmmoCount;
        }
    }
}
