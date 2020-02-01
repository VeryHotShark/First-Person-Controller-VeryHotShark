using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public class InteractionController : PlayerComponent<InteractionController>
    {
        #region Variables    
            [Space, Header("Data")]
            [SerializeField] private InteractionData interactionData = null;

            [Space, Header("Ray Settings")]
            [SerializeField] private float rayDistance = 0f;
            [SerializeField] private float raySphereRadius = 0f;
            [SerializeField] private LayerMask interactableLayer = ~0;


            #region Private
                private Camera m_cam;

                private bool m_interacting;
                private float m_holdTimer = 0f;
                
            #endregion

        #endregion

        #region Built In Methods      
            private void Awake()
            {
                m_cam = FindObjectOfType<Camera>();
            }

            private void OnEnable()
            {
                InputManager._OnPlayerInteractPressed += _OnPlayerInteractPressed;
                InputManager._OnPlayerInteractReleased += _OnPlayerInteractReleased;
            }

            private void OnDisable()
            {
                InputManager._OnPlayerInteractPressed -= _OnPlayerInteractPressed;
                InputManager._OnPlayerInteractReleased -= _OnPlayerInteractReleased;
            }

            private void Update()
            {
                CheckForInteractable();
                CheckForInteractableInput();
            }
        #endregion

        #region Event Callback Methods    
            private void _OnPlayerInteractPressed()
            {
                m_interacting = true;
                m_holdTimer = 0f;
            }

            private void _OnPlayerInteractReleased()
            {
                m_interacting = false;
                m_holdTimer = 0f;
            }
        #endregion


        #region Custom methods         
            void CheckForInteractable()
            {
                Ray _ray = new Ray(m_cam.transform.position,m_cam.transform.forward);
                RaycastHit _hitInfo;

                bool _hitSomething = Physics.SphereCast(_ray,raySphereRadius, out _hitInfo, rayDistance, interactableLayer);

                if(_hitSomething)
                {
                    InteractableBase _interactable = _hitInfo.transform.GetComponent<InteractableBase>();

                    if(_interactable != null)
                    {
                        if(interactionData.IsEmpty())
                        {
                            interactionData.Interactable = _interactable;
                        }
                        else
                        {
                            if(!interactionData.IsSameInteractable(_interactable))
                            {
                                interactionData.Interactable = _interactable;
                            }  
                        }
                    }
                }
                else
                {
                    interactionData.ResetData();
                }

                Debug.DrawRay(_ray.origin,_ray.direction * rayDistance,_hitSomething ? Color.green : Color.red);
            }

            void CheckForInteractableInput()
            {
                if(interactionData.IsEmpty())
                    return;

                if(m_interacting)
                {
                    if(!interactionData.Interactable.IsInteractable)
                        return;

                    if(interactionData.Interactable.HoldInteract)
                    {
                        m_holdTimer += Time.deltaTime;

                        if(m_holdTimer >= interactionData.Interactable.HoldDuration)
                        {
                            interactionData.Interact();
                            m_interacting = false;
                        }
                    }
                    else
                    {
                        interactionData.Interact();
                        m_interacting = false;
                    }
                }
            }
        #endregion
    }
}
