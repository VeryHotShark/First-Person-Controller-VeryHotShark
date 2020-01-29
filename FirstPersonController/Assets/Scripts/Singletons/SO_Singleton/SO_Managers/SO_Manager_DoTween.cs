using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VHS
{
    [CreateAssetMenu(fileName = "SO_DoTweenManager", menuName = "SO_Managers/SO_DoTweenManager")]
    public class SO_Manager_DoTween : SO_Singleton<SO_Manager_DoTween>
    {
        [SerializeField] private bool m_autoKill = true;
        [SerializeField] private bool m_autoRecycable = true;
        [SerializeField] private bool m_autoPlay = true;
        
        public override void OnGameStart()
        {
            DOTween.defaultAutoKill = m_autoKill;
            DOTween.defaultRecyclable = m_autoRecycable;
            DOTween.defaultAutoPlay = m_autoPlay ? AutoPlay.All : AutoPlay.None;
        }
    }
}
