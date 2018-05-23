namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    //Responsible for the camera's secondary rotation
    public class ShipCameraRotator : MonoBehaviour
    {

        [SerializeField]
        Rigidbody target;

        [SerializeField]
        Transform myCamera;

        [Header("Auto Movement Variables")]
        [Header("  Movement")]
        [SerializeField]
        float pitchMovementMultiplier = 1f;
        [SerializeField]
        float yawMovementMultiplier = 1f;

        [Header("  Rotation")]
        [SerializeField]
        float pitchRotationMultiplier = 1f;
        [SerializeField]
        float yawRotationMultiplier = 1f;
        [SerializeField]
        float rollRotationMultiplier = 1f;

        [Space]

        [Header("Manual Movement Variables")]
        [SerializeField]
        float mouseSensitivity = 1.0f;
        [SerializeField]
        float maxHorizontalView;
        [SerializeField]
        float maxVerticalView;
        [SerializeField]
        float cameraResetTime;

        //State bools
        bool cameraNeedsReset = false;
        bool cameraIsResetting = false;

        //Rotation variables
        Vector3 autoRotationTarget;
        float xAxisRotation;
        float yAxisRotation;

        void Update()
        {
            OffsetCamera();
            RotateCamera();
        }

        void OffsetCamera()
        {
            //Move the camera slightly to accent movement
            myCamera.localPosition =
            new Vector3(
                Input.GetAxis("Yaw") * yawMovementMultiplier,
                -Input.GetAxis("Pitch") * pitchMovementMultiplier,
                0f
            );
        }

        void RotateCamera()
        {
            //Calculate automatic camera rotation
            autoRotationTarget = CalculateAutomaticRotation();

            //If camera is under player control
            if (Input.GetButton("Rotate Camera"))
            {
                //We'll need to reset the camera
                cameraNeedsReset = true;

                //Stop camera reset if one is in progress
                if (cameraIsResetting)
                {
                    StopAllCoroutines(); //Is this expensive?
                    cameraIsResetting = false;
                }

                //Take input
                yAxisRotation = Mathf.Clamp(yAxisRotation + (Input.GetAxis("Mouse X") * mouseSensitivity), -maxHorizontalView, maxHorizontalView);
                xAxisRotation = Mathf.Clamp(xAxisRotation + (-Input.GetAxis("Mouse Y") * mouseSensitivity), -maxVerticalView, maxVerticalView);

                //Apply rotation                (forward     +      manual rotation)
                myCamera.localRotation = Quaternion.identity * Quaternion.Euler(xAxisRotation, yAxisRotation, 0f);
            }
            else
            {
                //If the camera needs a reset, start the reset coroutine
                if (cameraNeedsReset)
                {
                    StartCoroutine(ResetCamera(cameraResetTime));
                }
                //If the camera is done resetting
                else if (!cameraIsResetting)
                {
                    //Apply the automatic rotation
                    myCamera.localRotation = Quaternion.Euler(autoRotationTarget);
                }

                //Update axis rotations to match auto rotation
                xAxisRotation = autoRotationTarget.x;
                yAxisRotation = autoRotationTarget.y;

            }
        }

        Vector3 CalculateAutomaticRotation()
        {
            //Use input for auto rotation

            //I'd use rotation velocity if i could, but quaternions are violence.
            Vector3 autoRotation = new Vector3(
                Input.GetAxis("Pitch") * pitchRotationMultiplier,
                Input.GetAxis("Yaw") * yawRotationMultiplier,
                Input.GetAxis("Roll") * rollRotationMultiplier
            );

            return autoRotation;
        }

        IEnumerator ResetCamera(float resetTime)
        {
            //Update state bools
            cameraNeedsReset = false;
            cameraIsResetting = true;

            //Get unchanging copies of the axis rotations
            float xAxisRotation = this.xAxisRotation;
            float yAxisRotation = this.yAxisRotation;

            //Start reset loop
            float timer = 0f;
            while (timer < resetTime)
            {
                timer += Time.deltaTime;

                //LERP towards the automatic rotation
                yAxisRotation = Mathf.Lerp(yAxisRotation, autoRotationTarget.y, (timer / resetTime));
                xAxisRotation = Mathf.Lerp(xAxisRotation, autoRotationTarget.x, (timer / resetTime));

                //Apply rotation
                myCamera.localRotation = Quaternion.identity * Quaternion.Euler(xAxisRotation, yAxisRotation, 0f);

                //Return for one frame
                yield return null;
            }

            //Apply automatic rotation to finish the reset
            myCamera.localRotation = Quaternion.Euler(autoRotationTarget);

            //Flag reset as complete
            cameraIsResetting = false;
        }

    }
}