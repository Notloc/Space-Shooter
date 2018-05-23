namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class MovingTarget : MonoBehaviour, iTargetable
    {
        [SerializeField]
        Vector3 velocity;

        [SerializeField]
        bool positionLocked;

        Vector3 startPos;
        Rigidbody myRigidbody;

        void Start()
        {
            myRigidbody = this.GetComponent<Rigidbody>();
            startPos = this.transform.position;
        }

        void LateUpdate()
        {
            if (positionLocked)
            {
                myRigidbody.transform.position = startPos;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            myRigidbody.velocity = velocity;
        }

        public Rigidbody GetRigidbody()
        {
            return myRigidbody;
        }
    }
}