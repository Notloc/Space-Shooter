namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerCannonAimer : MonoBehaviour, iProvideAim, iProvideTarget
    {
        Ray myAim;

        [SerializeField]
        float sensitvity = 2f;

        void Update()
        {
            Rotate();
            UpdateAim();
        }

        void Rotate()
        {
            float xMovement = Input.GetAxis("Mouse X") * sensitvity;
            float yMovement = -Input.GetAxis("Mouse Y") * sensitvity;

            this.transform.Rotate(new Vector3(yMovement, xMovement, 0f));
        }

        void UpdateAim()
        {
            myAim = new Ray(this.transform.position, this.transform.forward);
        }

        public Ray GetAim()
        {
            return myAim;
        }

        public Rigidbody GetTarget()
        {
            return null;
        }
    }
}