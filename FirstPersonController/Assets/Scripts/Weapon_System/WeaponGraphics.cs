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

        private LineRenderer m_lineRenderer;

        private void Awake()
        {
            m_initLocalPos = transform.localPosition;
            m_initLocalRot = transform.localRotation;

            m_lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        private void OnEnable()
        {
            Weapon.OnWeaponReloadPressed += OnWeaponReloadPressed;
            Weapon.OnWeaponShootSucceed += OnWeaponShootSucceed;

            UpdateManager._OnLateUpdate += _OnLateUpdate;
        }

        private void OnDisable()
        {
            Weapon.OnWeaponReloadPressed -= OnWeaponReloadPressed;
            Weapon.OnWeaponShootSucceed -= OnWeaponShootSucceed;

            UpdateManager._OnLateUpdate -= _OnLateUpdate;
        }

        private void OnWeaponReloadPressed()
        {
            if(Weapon.DuringReload) return;

            Vector3 _desiredRot = transform.localEulerAngles + Vector3.forward * 360f;

            m_reloadTween = transform.DOLocalRotate(_desiredRot, Weapon.Data.ReloadDuration, RotateMode.FastBeyond360)
                .OnStart(() => Weapon.OnWeaponReloadStarted())
                .OnComplete(() => Weapon.OnWeaponReloadCompleted());
        }

        private void OnWeaponShootSucceed()
        {
            // m_shootTween = transform.DOPunchRotation(Vector3.right * 30f, 0.4f);  
            m_shootTween = transform.DOPunchPosition(Vector3.back, 0.1f);
        }

        
        private void _OnLateUpdate(float _smoothDeltaTime)
        {
            if(!Weapon.DuringReload)
            {
                Vector3 _lookVector = (Weapon.CurrentAimPoint - Weapon.ProjectileAnchor.transform.position);
                Quaternion _desiredLookRot = Quaternion.LookRotation(_lookVector.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, _desiredLookRot, _smoothDeltaTime * 10f);

                float _lookVectorMagnitude = _lookVector.magnitude;
                m_lineRenderer.SetPosition(1, Vector3.forward * _lookVectorMagnitude);
            }
        }
    }
}
