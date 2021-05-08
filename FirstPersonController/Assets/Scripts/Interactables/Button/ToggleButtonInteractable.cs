using System;
using UnityEngine;
using UnityEngine.Events;

namespace VHS
{
    public class ToggleButtonInteractable : InteractableBase
    {
        [Serializable] private class ToggleButtonEvent : UnityEvent<ToggleButtonInteractable> { }

        [SerializeField] ToggleButtonEvent _toggleButtonEvent = default;

        public enum State
        {
            On,
            Off,
        }

        [SerializeField] State _buttonState = State.Off;
        public State ButtonState => _buttonState;
        public bool IsOn
        {
            get { return ButtonState == State.On; }
        }

        private void Awake()
        {
            _toggleButtonEvent?.Invoke(this);
        }

        public override void OnInteract()
        {
            base.OnInteract();

            this.Toggle();
            _toggleButtonEvent?.Invoke(this);
        }

        void Toggle()
        {
            if (ButtonState == State.Off) _buttonState = State.On;
            else if (ButtonState == State.On) _buttonState = State.Off;
        }
    }
}
