using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace VHS
{
    public class SO_Singleton_Manager : Singleton<SO_Singleton_Manager>
    {
        [SerializeField][Label("Managers")]
        private List<SO_Manager> so_managers = new List<SO_Manager>();

        private void OnEnable()
        {
            if(so_managers.Count == 0)
                return;

            foreach(SO_Manager so_manager in so_managers)
                so_manager?.OnGameStart();
        }

        private void OnDisable()
        {
            if(so_managers.Count == 0)
                return;

            foreach(SO_Manager so_manager in so_managers)
                so_manager?.OnGameEnd();
        }

    }
}
