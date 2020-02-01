using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class PlayerComponent<T> : BaseComponent where T : PlayerComponent<T>
    {
        private bool m_cached;

        private Player m_player;
        protected Player Player
        {
            get
            {
                if(!m_cached)
                {
                    m_cached = true;
                    m_player = GetComponentInParent<Player>();
                }

                return m_player;
            }
        }
    }

    public class BaseComponent : MonoBehaviour
    {
        public void Init() => Debug.Log("Player Component: " + this.GetType());
    }
}
