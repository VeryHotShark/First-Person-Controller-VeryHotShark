using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class Player : MonoBehaviour
    {
        private PlayerComponent[] m_playerComponents;
        
        public T FetchComponent<T>() where T : PlayerComponent
        {
            T _temp = default(T);

            foreach(PlayerComponent playerComponent in m_playerComponents)
            {
                if(playerComponent is T)
                    _temp = playerComponent as T;
            }

            return _temp;
        }

        private void Awake() => InitPlayerComponents();

        public void InitPlayerComponents()
        {
            m_playerComponents = GetComponentsInChildren<PlayerComponent>();

            foreach(PlayerComponent _baseComp in m_playerComponents)
                _baseComp.InitPlayerReference(this);
        }
    }
}
