namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    //Responsible for control of the camera's primary movement and primary orientation
    public class CameraController : MonoBehaviour
    {

        [Header("The Camera")]
        [SerializeField]
        Transform myCamera; //The camera itself
        [SerializeField]
        Transform cameraContainer; //Container transform for proper position + offset behaviour

        [Space]

        [Header("The Target")]
        [SerializeField]
        Transform target;   //What the camera follows

        [Space]


        [Header("Offsets")]
        [SerializeField]
        Vector3 positionOffset;
        [SerializeField]
        Vector3 rotationOffset;

        void Update()
        {
            if (target == null)
            {
                return;
            }

            MatchRotation();
            FollowTarget();
        }

        void FollowTarget()
        {
            this.transform.position = target.transform.position;
            cameraContainer.localPosition = positionOffset;
        }

        void MatchRotation()
        {
            //Match the targets rotation plus an offset
            this.transform.rotation = target.transform.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}