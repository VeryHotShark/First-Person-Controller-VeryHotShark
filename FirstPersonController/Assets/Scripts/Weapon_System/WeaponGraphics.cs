using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VHS
{
    public class WeaponGraphics : WeaponComponent<WeaponGraphics>
    {
        Tween m_reloadTween = null;
        Tween m_shootTween = null;

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
            m_reloadTween = transform.DOLocalRotate(Vector3.right * 360f, Weapon.Data.ReloadDuration, RotateMode.FastBeyond360).OnComplete(() => Weapon.OnWeaponReloadCompleted());
        }

        private void OnWeaponShootSucceed()
        {
            m_shootTween = transform.DOPunchRotation(Vector3.right * 30f, 1f);  
        }
    }
}
