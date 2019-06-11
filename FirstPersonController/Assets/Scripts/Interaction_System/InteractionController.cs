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
        #endregion

        #region Hover Data
            [BoxGroup("Hover Data")] public Material hoverMaterial;
        #endregion

        #region Variables
            [BoxGroup("Settings")] public int checkInterval;
            [BoxGroup("Settings")] public float rayDistance;
            [BoxGroup("Settings")] public LayerMask interactableLayer;

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


            void CheckForInteractable()
            {
                if(Time.frameCount % checkInterval == 0)
                {
                    Ray _ray = new Ray(transform.position,transform.forward);
                    bool _hitSomething = Physics.Raycast(_ray,out m_hitInfo,rayDistance,interactableLayer);

                    if(_hitSomething)
                    {
                        InteractableBase _interactable = m_hitInfo.transform.GetComponent<InteractableBase>();
                        // Hoverable _hoverable = m_hitInfo.transform.GetComponent<Hoverable>();

                        if(_interactable != null /* && _hoverable != null */)
                        {
                            if(!interactionData.IsSameInteractable(_interactable))
                            {
                                interactionData.Interactable = _interactable;

                                if(interactionData.Interactable.IsInteractable())
                                {    
                                    //_hoverable.OnHoverStart(hoverMaterial);

                                    m_interactionUI.SetToolTip(_interactable.TooltipTransform,_interactable.Tooltip,0f);
                                    m_interactionUI.SetTooltipActiveState(true);
                                }
                            }
                            else
                            {
                                if(interactionData.Interactable.IsInteractable())
                                {
                                    if(!m_interactionUI.IsTooltipActive())
                                    {
                                        m_interactionUI.SetToolTip(_interactable.TooltipTransform,_interactable.Tooltip,0f);
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

                            interactionData.Interactable = null;
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
