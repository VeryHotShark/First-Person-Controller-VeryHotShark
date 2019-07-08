using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace VHS
{
    public class Hoverable : MonoBehaviour, IHoverable
    {
        
        [BoxGroup("Settings")] public string tooltip;
        [BoxGroup("Settings")] public Transform tooltipTransform;

        private Material m_myMat;
        public Material MyMaterial => m_myMat;

        private MeshRenderer m_meshRenderer;
        private MeshRenderer MeshRenderer => m_meshRenderer;

        public string Tooltip
        {
            get => tooltip;
            set => tooltip = value;
        }

        public Transform TooltipTransform => tooltipTransform;


        protected virtual void Awake()
        {
            GetComponents();
        }

        protected virtual void GetComponents()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
            m_myMat = m_meshRenderer.material;
        }

        public void OnHoverStart(Material _hoverMat)
        {
            m_meshRenderer.material = _hoverMat;
        }

        public void OnHoverEnd()
        {
            m_meshRenderer.material = m_myMat;
        }

    }
}
