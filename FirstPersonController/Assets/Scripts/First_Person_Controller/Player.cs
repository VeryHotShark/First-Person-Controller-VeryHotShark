using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class Player : MonoBehaviour
    {
        private BaseComponent[] m_playerComponents;
        
        public T FetchComponent<T>() where T : BaseComponent
        {
            T temp = default(T);

            foreach(BaseComponent baseComp in m_playerComponents)
            {
                if(baseComp is T)
                    temp = baseComp as T;
            }

            return temp;
        }

        private void Awake() => InitPlayerComponents();

        public void InitPlayerComponents()
        {
            m_playerComponents = GetComponentsInChildren<BaseComponent>();

            foreach(BaseComponent _baseComp in m_playerComponents)
                _baseComp.Init();
        }
    }
}
