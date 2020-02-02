using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class WeaponController : PlayerComponent
    {
        private Weapon[] m_weapons;
        private AimController m_aimController;

        private bool m_shootHeld;

        private void Awake()
        {
            m_weapons = GetComponentsInChildren<Weapon>();
        }

        private void Start()
        {    
            m_aimController = Player.FetchComponent<AimController>();
        }

        private void OnEnable()
        {
            InputManager._OnPlayerShootPressed += _OnPlayerShootPressed;
            InputManager._OnPlayerShootReleased += _OnPlayerShootReleased;
            InputManager._OnPlayerReloadPressed += _OnPlayerReloadPressed;
        }

        private void OnDisable()
        {
            InputManager._OnPlayerShootPressed -= _OnPlayerShootPressed;
            InputManager._OnPlayerShootReleased -= _OnPlayerShootReleased;
            InputManager._OnPlayerReloadPressed -= _OnPlayerReloadPressed;
        }

        private void _OnPlayerShootPressed()
        {
            m_shootHeld = true;

            foreach (Weapon weapon in m_weapons)
                weapon.OnShootButtonPressed();
        }

        private void _OnPlayerShootReleased()
        {
            m_shootHeld = false;

            foreach (Weapon weapon in m_weapons)
                weapon.OnShootButtonReleased();
        }

        private void _OnPlayerReloadPressed()
        {
            foreach (Weapon weapon in m_weapons)
                weapon.OnReloadButtonPressed();
        }

        private void Update() => OnWeaponTick();

        private void OnWeaponTick()
        {
            if (m_shootHeld)
            {
                foreach (Weapon weapon in m_weapons)
                    weapon.OnShootButtonHeld();
            }

            foreach (Weapon weapon in m_weapons)
                weapon.CurrentAimPoint = m_aimController.AimPoint;
        }

    }
}
