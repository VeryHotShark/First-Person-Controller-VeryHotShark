using UnityEngine;
using NaughtyAttributes;

namespace VHS
{    
    public class InputHandler : MonoBehaviour
    {
        #region Data
            [BoxGroup("Input Data")]
            public CameraInputData cameraInputData;
            [BoxGroup("Input Data")]
            public MovementInputData movementInputData;
            [BoxGroup("Input Data")]
            public InteractionInputData interactionInputData;
            [BoxGroup("Input Data")]
            public PickableInputData pickableInputData;
        #endregion

        #region BuiltIn Methods
            void Start()
            {
                cameraInputData.ResetInput();
                movementInputData.ResetInput();
            }

            void Update()
            {
                GetInteractionInput();
                GetPickableInput();
                GetCameraInput();
                GetMovementInputData();
            }
        #endregion

        #region Custom Methods
            void GetInteractionInput()
            {
                interactionInputData.Interact = Input.GetKeyDown(KeyCode.E);
                interactionInputData.HoldInteract = Input.GetKey(KeyCode.E);
            }

            void GetPickableInput()
            {
                pickableInputData.PickClicked = Input.GetMouseButtonDown(0);
                pickableInputData.PickHold = Input.GetMouseButton(0);
                pickableInputData.PickReleased = Input.GetMouseButtonUp(0);
            }

            void GetCameraInput()
            {
                cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
                cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");

                cameraInputData.ZoomClicked = Input.GetMouseButtonDown(1);
                cameraInputData.ZoomReleased = Input.GetMouseButtonUp(1);
            }

            void GetMovementInputData()
            {
                movementInputData.InputVectorX = Input.GetAxisRaw("Horizontal");
                movementInputData.InputVectorY = Input.GetAxisRaw("Vertical");

                //movementInputData.IsRunning = Input.GetKey(KeyCode.LeftShift);
                movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
                movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);

                if(movementInputData.RunClicked)
                    movementInputData.IsRunning = true;

                if(movementInputData.RunReleased)
                    movementInputData.IsRunning = false;

                movementInputData.JumpClicked = Input.GetKeyDown(KeyCode.Space);
                movementInputData.CrouchClicked = Input.GetKeyDown(KeyCode.LeftControl);
            }
        #endregion
    }
}