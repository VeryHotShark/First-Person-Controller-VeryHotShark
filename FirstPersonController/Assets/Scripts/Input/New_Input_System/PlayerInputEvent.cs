using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VHS
{
    public class PlayerInputEvent : Singleton<PlayerInputEvent>
    {
        public static InputEvent _MeleeInput { get; private set; }
        public static InputEvent _ThrowInput { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            InitInputEvents();
        }

        private void OnEnable() => UpdateManager._OnUpdate += _OnUpdate;
        private void OnDisable() => UpdateManager._OnUpdate -= _OnUpdate;
        private void _OnUpdate(float _dt) => InputEvent.UpdateKeyStates();

        private void InitInputEvents()
        {
            Keyboard _currentKeyboard = Keyboard.current;

            _MeleeInput = new InputEvent(_currentKeyboard.vKey);
            _ThrowInput = new InputEvent(_currentKeyboard.fKey);
        }
    }
}
