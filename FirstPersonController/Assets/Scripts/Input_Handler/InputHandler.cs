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

        #endregion

        #region BuiltIn Methods
            protected virtual void Start()
            {
                cameraInputData.ResetInput();
                movementInputData.ResetInput();
            }

            protected virtual void Update()
            {
                GetCameraInput();
                GetMovementInputData();
            }
        #endregion

        #region Custom Methods
            protected virtual void GetCameraInput()
            {
                cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
                cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");

                cameraInputData.ZoomClicked = Input.GetMouseButtonDown(1);
                cameraInputData.ZoomReleased = Input.GetMouseButtonUp(1);
            }

            protected virtual void GetMovementInputData()
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