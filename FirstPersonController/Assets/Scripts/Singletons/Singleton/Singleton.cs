using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;

        public static T Instance
        {
            get
            {
                if(m_instance == null)
                    m_instance = FindObjectOfType<T>();

                if(m_instance == null)
                {
                    GameObject _instance = new GameObject(typeof(T).Name);
                    m_instance = _instance.AddComponent<T>();
                }

                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if(m_instance == null)
                m_instance = this as T;
            else
                Destroy(this.gameObject);
        }
    }
}
