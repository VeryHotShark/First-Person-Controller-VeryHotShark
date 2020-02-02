using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class PlayerComponent : MonoBehaviour
    {
        private Player m_player;
        protected Player Player => m_player;

        public void InitPlayerReference(Player player) => m_player = player;
    }
}
