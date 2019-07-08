using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace VHS
{    
    public class InteractionUI : MonoBehaviour
    {
        public Image holdProgressIMG;
        public Image tooltipBG;
        private RectTransform m_canvasTransform;
        private TextMeshProUGUI m_interactableTooltip;

        public void Init()
        {
            GetComponents();
        }

        void GetComponents()
        {
            m_canvasTransform = GetComponent<RectTransform>();
            m_interactableTooltip = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetToolTip(Transform _parent , string _tooltip, float _holdProgress)
        {
            if(_parent)
            {
                m_canvasTransform.position = _parent.position;
                m_canvasTransform.SetParent(_parent);
            }

            m_interactableTooltip.SetText(_tooltip);
            holdProgressIMG.fillAmount = _holdProgress;
        }

        public void SetTooltipActiveState(bool _state)
        {
            m_interactableTooltip.gameObject.SetActive(_state);
            holdProgressIMG.gameObject.SetActive(_state);
            tooltipBG.gameObject.SetActive(_state);
        }

        public void UpdateChargeProgress(float _progress)
        {
            holdProgressIMG.fillAmount = _progress;
        }

        public void LookAtPlayer(Transform _player)
        {
            m_canvasTransform.LookAt(_player,Vector3.up);
        }

        public void UnparentToltip()
        {
            m_canvasTransform.SetParent(null);
        }

        public bool IsTooltipActive()
        {
            return m_interactableTooltip.gameObject.activeSelf;
        }

    }
}