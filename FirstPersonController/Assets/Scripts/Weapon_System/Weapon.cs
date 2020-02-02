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

        public WeaponData Data => weaponData;
        public Projectile Projectile => projectile;

        private WeaponProjectileAnchor m_projectileAnchor;
        public WeaponProjectileAnchor ProjectileAnchor => m_projectileAnchor;

        private int m_currentAmmoCount;
        private float m_timeSinceLastShot;
        private bool m_duringReload;
        private Vector3 m_currentAimPoint;

        public int CurrentAmmoCount
        {
            get => m_currentAmmoCount;
            set => m_currentAmmoCount = value;
        }

        public float TimeSinceLastShot
        {
            get => m_timeSinceLastShot;
            set => m_timeSinceLastShot = value;
        }
        public bool DuringReload
        {
            get => m_duringReload;
            set => m_duringReload = value;
        }

        public Vector3 CurrentAimPoint
        {
            get => m_currentAimPoint;
            set => m_currentAimPoint = value;
        }

        public Action OnWeaponShootPressed = delegate { };
        public Action OnWeaponShootHeld = delegate { };
        public Action OnWeaponShootReleased = delegate { };
        public Action OnWeaponShootSucceed = delegate { };

        public Action OnWeaponReloadPressed = delegate { };
        public Action OnWeaponReloadStarted = delegate { };
        public Action OnWeaponReloadCompleted = delegate { };

        public virtual void OnShootButtonPressed() => OnWeaponShootPressed();
        public virtual void OnShootButtonHeld() => OnWeaponShootHeld();
        public virtual void OnShootButtonReleased() => OnWeaponShootReleased();
        public virtual void OnReloadButtonPressed() => OnWeaponReloadPressed();

        public virtual void Awake()
        {
            InitData();
            GetComponents();
        }

        public virtual void InitData()
        {
            Data.Init();
            CurrentAmmoCount = Data.AmmoCount;
        }

        public virtual void GetComponents()
        {
            m_projectileAnchor = GetComponentInChildren<WeaponProjectileAnchor>();
        }
    }

    public class WeaponComponent<T> : MonoBehaviour where T : WeaponComponent<T>
    {
        private bool m_cached;

        private Weapon m_weapon;
        public Weapon Weapon
        {
            get
            {
                if (!m_cached)
                {
                    m_cached = true;
                    m_weapon = GetComponentInParent<Weapon>();
                }

                return m_weapon;
            }
        }
    }
}
