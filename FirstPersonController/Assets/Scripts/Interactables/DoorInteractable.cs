using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace VHS
{
    public class DoorInteractable : InteractableBase
    {
        public enum DoorState
        {
            Close,
            Open
        }

        [BoxGroup("Door Settings")] public float rotateAngle = 90f;
        [BoxGroup("Door Settings")] public float rotateDuration = 0.75f;
        [BoxGroup("Door Settings")] public AnimationCurve rotateCurve;

        private DoorState m_doorState;

        Quaternion m_initRot;

        void Start()
        {
            m_initRot = transform.rotation;
        }

        public override void OnInteract()
        {
            base.OnInteract();

            InteractDoor();
        }

        private void InteractDoor()
        {
            StartCoroutine(InteractDoorRoutine());
        }

        private IEnumerator InteractDoorRoutine()
        {
            m_interactable = false;

            float _percent = 0f;
            float _speed = 1f / rotateDuration;

            Quaternion _currentRot = transform.rotation;
            Quaternion _desiredRot;

            if(m_doorState == DoorState.Close)
                _desiredRot = Quaternion.Euler(m_initRot.eulerAngles + (Vector3.up * rotateAngle));
            else
                _desiredRot = m_initRot;

            if(m_doorState == DoorState.Open)
            {
                // Tooltip = "Open Door";
                m_doorState = DoorState.Close;
            }
            else
            {
                // Tooltip = "Close Door";
                m_doorState = DoorState.Open;
            }

            while(_percent < 1f)
            {
                _percent += Time.deltaTime * _speed;
                
                transform.rotation = Quaternion.Slerp(_currentRot,_desiredRot,rotateCurve.Evaluate(_percent));

                yield return null;
            }

            m_interactable = true;
        }
    }
}
