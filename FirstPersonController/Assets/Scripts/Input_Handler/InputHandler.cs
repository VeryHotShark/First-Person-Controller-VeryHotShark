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
<<<<<<< HEAD
            [BoxGroup("Input Data")]
            public PickableInputData pickableInputData;
=======
>>>>>>> parent of 391627a... Revert "InteractionSystem"
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
<<<<<<< HEAD
                GetPickableInput();
=======
>>>>>>> parent of 391627a... Revert "InteractionSystem"
                GetCameraInput();
                GetMovementInputData();
            }
        #endregion

        #region Custom Methods
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 391627a... Revert "InteractionSystem"
            void GetInteractionInput()
            {
                interactionInputData.Interact = Input.GetKeyDown(KeyCode.E);
                interactionInputData.HoldInteract = Input.GetKey(KeyCode.E);
            }

<<<<<<< HEAD
            void GetPickableInput()
            {
                pickableInputData.PickClicked = Input.GetMouseButtonDown(0);
                pickableInputData.PickHold = Input.GetMouseButton(0);
                pickableInputData.PickReleased = Input.GetMouseButtonUp(0);
            }

=======
>>>>>>> parent of e1cbcd5... Marked all methods as virtual, marked private fields and methods as Protected to allow for extension via Inheritance
=======
>>>>>>> parent of 45e9ca2... Merge pull request #2 from beardordie/master
=======
>>>>>>> parent of 391627a... Revert "InteractionSystem"
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