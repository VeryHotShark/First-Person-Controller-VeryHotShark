using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VHS
{
    [CreateAssetMenu(fileName = "WeaponInputData", menuName = "FirstPersonController/Data/WeaponInputData")]
    public class WeaponInputData : ScriptableObject
    {
        private bool m_shootButtonClicked;
        private bool m_shootButtonReleased;

        private bool m_reloadButtonClicked;
        private bool m_reloadButtonReleased;

        public bool ShootButtonClicked
        { 
            get => m_shootButtonClicked; 
            set => m_shootButtonClicked = value; 
        }

        public bool ShootButtonReleased
        { 
            get => m_shootButtonReleased;
            set => m_shootButtonReleased = value; 
        }

        public bool ReloadButtonClicked
        { 
            get => m_reloadButtonClicked; 
            set => m_reloadButtonClicked = value; 
        }

        public bool ReloadButtonReleased
        { 
            get => m_reloadButtonReleased;
            set => m_reloadButtonReleased = value; 
        }

        public void ResetInput()
        {
            m_shootButtonClicked = false;
            m_shootButtonReleased = false;

            m_reloadButtonClicked = false;
            m_reloadButtonReleased = false;
        }
    }
}
