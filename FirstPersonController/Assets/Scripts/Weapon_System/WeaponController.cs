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
            foreach(Weapon weapon in m_weapons)
            {
                weapon.OnUpdate();

                switch(weapon.Data.TriggerType)
                {
                    case WeaponTriggerType.PullRelease :
                    {
                        if(weaponInputData.ShootButtonClicked)
                            weapon.OnWeaponShootAttempt();

                        break;
                    }
                    case WeaponTriggerType.Continous :
                    {
                        if(weaponInputData.ShootButtonHeld)
                            weapon.OnWeaponShootAttempt();

                        break;
                    }
                }

                if(weaponInputData.ReloadButtonClicked)
                    weapon.OnReloadWeaponPressed();
            }
        }

    }
}
