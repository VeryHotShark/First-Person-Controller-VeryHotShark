// GENERATED AUTOMATICALLY FROM 'Assets/Input_Assets/Player_Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player_Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player_Input"",
    ""maps"": [
        {
            ""name"": ""Player_Gameplay"",
            ""id"": ""5a77a03f-e863-4c71-827e-245e3e9f2d40"",
            ""actions"": [
                {
                    ""name"": ""Player_Basic_Movement"",
                    ""type"": ""Value"",
                    ""id"": ""e84d95b8-64d0-4e68-85e9-581bb69a5410"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Look"",
                    ""type"": ""Value"",
                    ""id"": ""20f9b501-fdb2-4093-8ac2-648314f1936a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Interact"",
                    ""type"": ""Button"",
                    ""id"": ""6b3a080f-1dcf-47a2-bfd5-242286482b1a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""1ec182f1-8efd-4734-9f3c-27f2a2ca54be"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""23011877-2c8b-4237-96c7-581ea76070b7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1780c046-c1ee-4413-8038-acb692127475"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""bae8415d-25b7-419a-9e44-2895c35ecfea"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""efd8002d-c571-4705-aa08-02d61f3699d5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Player_Reload"",
                    ""type"": ""Button"",
                    ""id"": ""3295ab70-6ba8-4bc1-ba88-34ca7e9ba6a0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""a9f88d43-87fe-4ea3-a4ee-8b9857ccd9f5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Player_Basic_Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5337142f-77b7-4868-a486-70b855d376ec"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Basic_Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""68b51e2e-baa6-4122-b683-6a320b313e3a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Basic_Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d0391f04-8fec-431e-9f97-4099e2a5d6de"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Basic_Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""83b2b868-5ae4-48ed-b17b-6d1c5924a115"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Basic_Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4b406ae0-a926-44d9-823c-011471e1e957"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f74029d-9e47-41ca-aba6-82e995ea0c83"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""PressRelease"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29eb1ae3-5624-42ab-9caf-1d9dba51b69a"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""PressRelease"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afe8ab63-75d1-4bdb-86c7-d70529b7005c"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""PressRelease"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5d5e6aa-1dd0-43d5-aee1-190e29244892"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d9d88c7-e60f-450a-bfd7-95dfe18433f2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""PressRelease"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84b6ca70-2d94-41bc-a85d-62406753474b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""PressRelease"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be55e2ef-76fd-41a4-a63a-f3c254257631"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Player_Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player_Gameplay
        m_Player_Gameplay = asset.FindActionMap("Player_Gameplay", throwIfNotFound: true);
        m_Player_Gameplay_Player_Basic_Movement = m_Player_Gameplay.FindAction("Player_Basic_Movement", throwIfNotFound: true);
        m_Player_Gameplay_Player_Look = m_Player_Gameplay.FindAction("Player_Look", throwIfNotFound: true);
        m_Player_Gameplay_Player_Interact = m_Player_Gameplay.FindAction("Player_Interact", throwIfNotFound: true);
        m_Player_Gameplay_Player_Sprint = m_Player_Gameplay.FindAction("Player_Sprint", throwIfNotFound: true);
        m_Player_Gameplay_Player_Crouch = m_Player_Gameplay.FindAction("Player_Crouch", throwIfNotFound: true);
        m_Player_Gameplay_Player_Jump = m_Player_Gameplay.FindAction("Player_Jump", throwIfNotFound: true);
        m_Player_Gameplay_Player_Zoom = m_Player_Gameplay.FindAction("Player_Zoom", throwIfNotFound: true);
        m_Player_Gameplay_Player_Shoot = m_Player_Gameplay.FindAction("Player_Shoot", throwIfNotFound: true);
        m_Player_Gameplay_Player_Reload = m_Player_Gameplay.FindAction("Player_Reload", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player_Gameplay
    private readonly InputActionMap m_Player_Gameplay;
    private IPlayer_GameplayActions m_Player_GameplayActionsCallbackInterface;
    private readonly InputAction m_Player_Gameplay_Player_Basic_Movement;
    private readonly InputAction m_Player_Gameplay_Player_Look;
    private readonly InputAction m_Player_Gameplay_Player_Interact;
    private readonly InputAction m_Player_Gameplay_Player_Sprint;
    private readonly InputAction m_Player_Gameplay_Player_Crouch;
    private readonly InputAction m_Player_Gameplay_Player_Jump;
    private readonly InputAction m_Player_Gameplay_Player_Zoom;
    private readonly InputAction m_Player_Gameplay_Player_Shoot;
    private readonly InputAction m_Player_Gameplay_Player_Reload;
    public struct Player_GameplayActions
    {
        private @Player_Input m_Wrapper;
        public Player_GameplayActions(@Player_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Player_Basic_Movement => m_Wrapper.m_Player_Gameplay_Player_Basic_Movement;
        public InputAction @Player_Look => m_Wrapper.m_Player_Gameplay_Player_Look;
        public InputAction @Player_Interact => m_Wrapper.m_Player_Gameplay_Player_Interact;
        public InputAction @Player_Sprint => m_Wrapper.m_Player_Gameplay_Player_Sprint;
        public InputAction @Player_Crouch => m_Wrapper.m_Player_Gameplay_Player_Crouch;
        public InputAction @Player_Jump => m_Wrapper.m_Player_Gameplay_Player_Jump;
        public InputAction @Player_Zoom => m_Wrapper.m_Player_Gameplay_Player_Zoom;
        public InputAction @Player_Shoot => m_Wrapper.m_Player_Gameplay_Player_Shoot;
        public InputAction @Player_Reload => m_Wrapper.m_Player_Gameplay_Player_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Player_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_GameplayActions instance)
        {
            if (m_Wrapper.m_Player_GameplayActionsCallbackInterface != null)
            {
                @Player_Basic_Movement.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Basic_Movement;
                @Player_Basic_Movement.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Basic_Movement;
                @Player_Basic_Movement.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Basic_Movement;
                @Player_Look.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Look;
                @Player_Look.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Look;
                @Player_Look.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Look;
                @Player_Interact.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Interact;
                @Player_Interact.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Interact;
                @Player_Interact.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Interact;
                @Player_Sprint.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Sprint;
                @Player_Sprint.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Sprint;
                @Player_Sprint.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Sprint;
                @Player_Crouch.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Crouch;
                @Player_Crouch.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Crouch;
                @Player_Crouch.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Crouch;
                @Player_Jump.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Jump;
                @Player_Jump.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Jump;
                @Player_Jump.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Jump;
                @Player_Zoom.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Zoom;
                @Player_Zoom.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Zoom;
                @Player_Zoom.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Zoom;
                @Player_Shoot.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Shoot;
                @Player_Shoot.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Shoot;
                @Player_Shoot.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Shoot;
                @Player_Reload.started -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Reload;
                @Player_Reload.performed -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Reload;
                @Player_Reload.canceled -= m_Wrapper.m_Player_GameplayActionsCallbackInterface.OnPlayer_Reload;
            }
            m_Wrapper.m_Player_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Player_Basic_Movement.started += instance.OnPlayer_Basic_Movement;
                @Player_Basic_Movement.performed += instance.OnPlayer_Basic_Movement;
                @Player_Basic_Movement.canceled += instance.OnPlayer_Basic_Movement;
                @Player_Look.started += instance.OnPlayer_Look;
                @Player_Look.performed += instance.OnPlayer_Look;
                @Player_Look.canceled += instance.OnPlayer_Look;
                @Player_Interact.started += instance.OnPlayer_Interact;
                @Player_Interact.performed += instance.OnPlayer_Interact;
                @Player_Interact.canceled += instance.OnPlayer_Interact;
                @Player_Sprint.started += instance.OnPlayer_Sprint;
                @Player_Sprint.performed += instance.OnPlayer_Sprint;
                @Player_Sprint.canceled += instance.OnPlayer_Sprint;
                @Player_Crouch.started += instance.OnPlayer_Crouch;
                @Player_Crouch.performed += instance.OnPlayer_Crouch;
                @Player_Crouch.canceled += instance.OnPlayer_Crouch;
                @Player_Jump.started += instance.OnPlayer_Jump;
                @Player_Jump.performed += instance.OnPlayer_Jump;
                @Player_Jump.canceled += instance.OnPlayer_Jump;
                @Player_Zoom.started += instance.OnPlayer_Zoom;
                @Player_Zoom.performed += instance.OnPlayer_Zoom;
                @Player_Zoom.canceled += instance.OnPlayer_Zoom;
                @Player_Shoot.started += instance.OnPlayer_Shoot;
                @Player_Shoot.performed += instance.OnPlayer_Shoot;
                @Player_Shoot.canceled += instance.OnPlayer_Shoot;
                @Player_Reload.started += instance.OnPlayer_Reload;
                @Player_Reload.performed += instance.OnPlayer_Reload;
                @Player_Reload.canceled += instance.OnPlayer_Reload;
            }
        }
    }
    public Player_GameplayActions @Player_Gameplay => new Player_GameplayActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayer_GameplayActions
    {
        void OnPlayer_Basic_Movement(InputAction.CallbackContext context);
        void OnPlayer_Look(InputAction.CallbackContext context);
        void OnPlayer_Interact(InputAction.CallbackContext context);
        void OnPlayer_Sprint(InputAction.CallbackContext context);
        void OnPlayer_Crouch(InputAction.CallbackContext context);
        void OnPlayer_Jump(InputAction.CallbackContext context);
        void OnPlayer_Zoom(InputAction.CallbackContext context);
        void OnPlayer_Shoot(InputAction.CallbackContext context);
        void OnPlayer_Reload(InputAction.CallbackContext context);
    }
}
