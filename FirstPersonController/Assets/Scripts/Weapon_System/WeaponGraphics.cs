using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VHS
{
    public class WeaponGraphics : WeaponComponent<WeaponGraphics>
    {
        private Tween m_reloadTween = null;
        private Tween m_shootTween = null;

        private Vector3 m_initLocalPos;
        private Quaternion m_initLocalRot;

        private void Awake()
        {
            m_initLocalPos = transform.localPosition;
            m_initLocalRot = transform.localRotation;
        }

        private void OnEnable()
        {
            Weapon.OnWeaponReloadPressed += OnWeaponReloadPressed;
            Weapon.OnWeaponShootSucceed += OnWeaponShootSucceed;
        }

        private void OnDisable()
        {
            Weapon.OnWeaponReloadPressed -= OnWeaponReloadPressed;
            Weapon.OnWeaponShootSucceed -= OnWeaponShootSucceed;
        }

        private void OnWeaponReloadPressed()
        {
            if(Weapon.DuringReload) return;

            m_reloadTween = transform.DOLocalRotate(Vector3.right * 360f, Weapon.Data.ReloadDuration, RotateMode.FastBeyond360)
                .OnStart(() => Weapon.OnWeaponReloadStarted())
                .OnComplete(() => Weapon.OnWeaponReloadCompleted());
        }

        private void OnWeaponShootSucceed()
        {
            // m_shootTween = transform.DOPunchRotation(Vector3.right * 30f, 0.4f);  
            m_shootTween = transform.DOPunchPosition(Vector3.back, 0.1f);
        }
    }
}
