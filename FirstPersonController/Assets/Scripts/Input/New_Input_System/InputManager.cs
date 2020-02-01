using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace VHS
{
    public class InputManager : Singleton<InputManager>, Player_Input.IPlayer_GameplayActions
    {
        private Player_Input playerInput;

        public static Vector2 _MovementAxis { get; private set; }
        public static Vector2 _LookAxis { get; private set; }

        public static event Action _OnPlayerInteractPressed = delegate { };
        public static event Action _OnPlayerInteractReleased = delegate { };

        public static event Action _OnPlayerSprintPressed = delegate { };
        public static event Action _OnPlayerSprintReleased = delegate { };

        public static event Action _OnPlayerCrouchPressed = delegate { };
        public static event Action _OnPlayerCrouchReleased = delegate { };

        public static event Action _OnPlayerZoomPressed = delegate { };
        public static event Action _OnPlayerZoomReleased = delegate { };

        public static event Action _OnPlayerShootPressed = delegate { };
        public static event Action _OnPlayerShootReleased = delegate { };

        public static event Action _OnPlayerReloadPressed = delegate { };
        public static event Action _OnPlayerJumpPressed = delegate { };

        private void OnEnable()
        {
            playerInput = new Player_Input();
            playerInput.Player_Gameplay.SetCallbacks(this);
            playerInput.Player_Gameplay.Enable();
        }

        private void OnDisable()
        {
            playerInput.Player_Gameplay.Disable();
        }

        public void OnPlayer_Basic_Movement(InputAction.CallbackContext context) => _MovementAxis = context.ReadValue<Vector2>();
        public void OnPlayer_Look(InputAction.CallbackContext context) => _LookAxis = context.ReadValue<Vector2>();

        public void OnPlayer_Interact(InputAction.CallbackContext context)
        {
            if (context.started)
                _OnPlayerInteractPressed();

            if (context.canceled)
                _OnPlayerInteractReleased();
        }

        public void OnPlayer_Sprint(InputAction.CallbackContext context)
        {
            if (context.started)
                _OnPlayerSprintPressed();

            if (context.canceled)
                _OnPlayerSprintReleased();
        }

        public void OnPlayer_Crouch(InputAction.CallbackContext context)
        {
            if (context.started)
                _OnPlayerCrouchPressed();

            if (context.canceled)
                _OnPlayerCrouchReleased();
        }

        public void OnPlayer_Zoom(InputAction.CallbackContext context)
        {
            if (context.started)
                _OnPlayerZoomPressed();

            if (context.canceled)
                _OnPlayerZoomReleased();
        }

        public void OnPlayer_Jump(InputAction.CallbackContext context)
        {
            if (context.performed) 
                _OnPlayerJumpPressed();
        }

        public void OnPlayer_Shoot(InputAction.CallbackContext context)
        {
            if (context.started)
                _OnPlayerShootPressed();

            if (context.canceled)
                _OnPlayerShootReleased();
        }

        public void OnPlayer_Reload(InputAction.CallbackContext context)
        {
            if(context.performed)
                _OnPlayerReloadPressed();
        }
    }
}
