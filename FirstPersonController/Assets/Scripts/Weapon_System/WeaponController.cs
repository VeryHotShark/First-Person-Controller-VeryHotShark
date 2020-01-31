using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private WeaponInputData weaponInputData = null;

        private Weapon[] m_weapons;

        private void Awake()
        {
            m_weapons = GetComponentsInChildren<Weapon>();
        }

        private void Update()
        {
            OnWeaponTick();
        }

        private void OnWeaponTick()
        { 
            if(weaponInputData.ShootButtonClicked)
            {
                foreach(Weapon weapon in m_weapons)
                    weapon.OnShootButtonPressed();
            }

            if(weaponInputData.ShootButtonHeld)
            {
                foreach(Weapon weapon in m_weapons)
                    weapon.OnShootButtonHeld();
            }

            if(weaponInputData.ShootButtonReleased)
            {
                foreach(Weapon weapon in m_weapons)
                    weapon.OnShootButtonReleased();
            }

            if(weaponInputData.ReloadButtonClicked)
            {
                foreach(Weapon weapon in m_weapons)
                    weapon.OnReloadButtonPressed();
            }
        }

    }
}
