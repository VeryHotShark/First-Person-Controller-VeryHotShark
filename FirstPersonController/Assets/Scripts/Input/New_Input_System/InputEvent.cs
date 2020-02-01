using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace VHS
{
    public class InputEvent
    {
        private static List<InputEvent> m_inputEvents = new List<InputEvent>();

        public static void UpdateKeyStates()
        {
            foreach(InputEvent _inputEvent in m_inputEvents)
                _inputEvent.UpdateKeyState();
        }

        private KeyControl m_keyControl;

        public InputEvent(KeyControl _keyControl)
        {
            m_keyControl = _keyControl;
            m_inputEvents.Add(this);
        }

        private bool m_pressedLastFrame = false;
        private float m_lastKeyPressedTimeStamp = 0.0f;

        public bool Pressed { get; private set; }
        public bool Held { get; private set; }
        public bool Released { get; private set; }
        public bool DoubleTapped { get; private set; }

        public event Action OnPressed = delegate { };
        public event Action OnHeld = delegate { };
        public event Action OnReleased = delegate { };
        public event Action OnDoubleTapped = delegate { };

        private void UpdateKeyState()
        {
            bool _pressedThisFrame = m_keyControl.isPressed;

            Pressed = !m_pressedLastFrame && _pressedThisFrame;
            Held = m_pressedLastFrame && _pressedThisFrame;
            Released = m_pressedLastFrame && !_pressedThisFrame;
            DoubleTapped = false;

            if(Pressed)
            {
                DoubleTapped = Time.time - m_lastKeyPressedTimeStamp < SO_Manager_Input.Instance.DoubleTapThreshold;
                m_lastKeyPressedTimeStamp = Time.time;
            }

            HandleKeyEventCallbacks();

            m_pressedLastFrame = _pressedThisFrame;
        }

        private void HandleKeyEventCallbacks()
        {
            if(Pressed) OnPressed();
            if(Held) OnHeld();
            if(Released) OnReleased();
            if(DoubleTapped) OnDoubleTapped();
        }
    }
}
