using TMPro;
using UnityEngine;
using NaughtyAttributes;

namespace VHS
{    
    public class InteractionController : MonoBehaviour
    {
        #region Data
            [BoxGroup("Data")] public InteractionInputData interactionInputData;
            [BoxGroup("Data")] public InteractionData interactionData;
            [BoxGroup("Data")] public PickableInputData pickableInputData;
            [BoxGroup("Data")] public PickableData pickableData;
        #endregion

        #region Hover Data
            [BoxGroup("Hover Data")] public Material hoverMaterial;
        #endregion

        #region Variables
            [BoxGroup("General Settings")] public float rayDistance;
            [BoxGroup("General Settings")] public int checkInterval;

            [BoxGroup("Interactable Settings")] public LayerMask interactableLayer;

            [BoxGroup("Pickable Settings")] public LayerMask pickableLayer ;
            [BoxGroup("Pickable Settings")] public float zDistance;

            #region Private
                RaycastHit m_hitInfo;
                InteractionUI m_interactionUI;
                float m_holdTimer = 0f;
            #endregion

            #region Properties
                public float HoldPercent
                {
                    get => Mathf.Clamp01(m_holdTimer/ interactionData.Interactable.HoldDuration);
                }
            #endregion
        #endregion


        #region BuiltIn Methods    
            // Start is called before the first frame update
            void Start()
            {
                GetComponents();
                m_interactionUI.Init();
                m_interactionUI.SetTooltipActiveState(false);
            }

            // Update is called once per frame
            void Update()
            {
                CheckForInteractable();
                CheckForPickable();
                CheckForInput();

                m_interactionUI.LookAtPlayer(transform);
            }
        #endregion

        #region Custom Methods
            void GetComponents()
            {
                m_interactionUI = FindObjectOfType<InteractionUI>();
            }

            void CheckForInput()
            {
                CheckForInteractableInput();
                CheckForPickableInput();
            }

            void CheckForInteractableInput()
            {
                if(interactionData.Interactable != null)
                {
                    if(interactionData.Interactable.IsInteractable())
                    {
                        if(!interactionData.Interactable.HoldInteract)
                        {
                            if(interactionInputData.Interact)
                            {
                                m_interactionUI.UnparentToltip();

                                interactionData.Interactable.OnInteract();
                                interactionData.ResetData();
                            }
                        }
                        else
                        {
                            if(interactionInputData.HoldInteract)
                            {
                                m_holdTimer += Time.deltaTime;

                                m_interactionUI.UpdateChargeProgress(HoldPercent);

                                if(m_holdTimer >= interactionData.Interactable.HoldDuration)
                                {
                                    m_holdTimer = 0f;

                                    m_interactionUI.UnparentToltip();
                                    m_interactionUI.UpdateChargeProgress(HoldPercent);
                                    m_interactionUI.SetTooltipActiveState(false);

                                    interactionData.Interactable.OnInteract();
                                    interactionData.ResetData();

                                }
                            }
                            else
                            {
                                m_holdTimer = 0f;
                                m_interactionUI.UpdateChargeProgress(HoldPercent);
                            }
                        }
                    }
                }
            }

            void CheckForPickableInput()
            {
                if(!pickableData.IsEmpty())
                {
                    if(pickableInputData.PickClicked)
                    {
                        m_interactionUI.SetToolTip(null,"",0f);
                        m_interactionUI.SetTooltipActiveState(false);

                        pickableData.PickableItem.Rigid.isKinematic = true;
                        pickableData.PickableItem.Rigid.useGravity = false;

                        pickableData.PickableItem.transform.position = transform.position + transform.forward * zDistance;
                        pickableData.PickableItem.transform.SetParent(transform);
                    }

                    if(pickableInputData.PickHold)
                    {
                        //pickableData.PickableItem.transform.position = transform.position + transform.forward * zDistance;
                    }

                    if(pickableInputData.PickReleased)
                    {
                        pickableData.PickableItem.transform.SetParent(null);

                        pickableData.PickableItem.Rigid.isKinematic = false;
                        pickableData.PickableItem.Rigid.useGravity = true;
                    }
                }
            }

            void CheckForInteractable()
            {
                if(Time.frameCount % checkInterval == 0)
                {
                    Ray _ray = new Ray(transform.position,transform.forward);
                    bool _hitSomething = Physics.Raycast(_ray,out m_hitInfo,rayDistance,interactableLayer);

                    if(_hitSomething)
                    {
                        InteractableBase _interactable = m_hitInfo.transform.GetComponent<InteractableBase>();
                        Hoverable _hoverable = m_hitInfo.transform.GetComponent<Hoverable>();

                        if(_interactable != null /* && _hoverable != null */)
                        {
                            if(!interactionData.IsSameInteractable(_interactable))
                            {
                                interactionData.Interactable = _interactable;

                                if(interactionData.Interactable.IsInteractable())
                                {    
                                    //_hoverable.OnHoverStart(hoverMaterial);

                                    m_interactionUI.SetToolTip(_hoverable.TooltipTransform,_hoverable.Tooltip,0f);
                                    m_interactionUI.SetTooltipActiveState(true);
                                }
                            }
                            else
                            {
                                if(interactionData.Interactable.IsInteractable())
                                {
                                    if(!m_interactionUI.IsTooltipActive())
                                    {
                                        m_interactionUI.SetToolTip(_hoverable.TooltipTransform,_hoverable.Tooltip,0f);
                                        m_interactionUI.SetTooltipActiveState(true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if(!interactionData.IsEmpty())
                        {
                            // Hoverable _hoverable = interactionData.Interactable.GetComponent<Hoverable>();

                            // if(_hoverable != null)
                            //     _hoverable.OnHoverEnd();

                            interactionData.ResetData();
                            m_interactionUI.SetToolTip(null,"",0f);
                            m_interactionUI.SetTooltipActiveState(false);
                        }
                    }

                    
                    Debug.DrawRay(transform.position,transform.forward,_hitSomething ? Color.green : Color.red);
                }
            }

            void CheckForPickable()
            {
                if(Time.frameCount % checkInterval == 0)
                {
                    Ray _ray = new Ray(transform.position,transform.forward);
                    bool _hitSomething = Physics.Raycast(_ray,out m_hitInfo,rayDistance,pickableLayer);

                    if(_hitSomething)
                    {
                        Pickable _pickable = m_hitInfo.transform.GetComponent<Pickable>();
                        Hoverable _hoverable = m_hitInfo.transform.GetComponent<Hoverable>();

                        if(_pickable != null /* && _hoverable != null */)
                        {
                            if(!pickableData.IsSamePickable(_pickable))
                            {
                                pickableData.PickableItem = _pickable;
                                m_interactionUI.SetToolTip(_hoverable.TooltipTransform,_hoverable.Tooltip,0f);
                                m_interactionUI.SetTooltipActiveState(true);
                            }
                        }
                    }
                    else
                    {
                        if(!pickableData.IsEmpty())
                        {
                            pickableData.ResetData();
                            m_interactionUI.SetToolTip(null,"",0f);
                            m_interactionUI.SetTooltipActiveState(false);
                        }
                    }

                    
                    Debug.DrawRay(transform.position,transform.forward,_hitSomething ? Color.green : Color.red);
                }
            }
        #endregion
    }
}
