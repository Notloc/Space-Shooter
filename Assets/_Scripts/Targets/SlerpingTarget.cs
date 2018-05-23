namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class SlerpingTarget : MonoBehaviour, iTargetable
    {

        [SerializeField]
        Transform startPosition;

        [SerializeField]
        Transform endPosition;

        [SerializeField]
        float speed;

        Rigidbody myRigidbody;

        bool movingForward = true;    //The direction we're moving
        float timer = 0f;       //how far along the slerp path we are [0, 1]

        void Start()
        {
            myRigidbody = this.GetComponent<Rigidbody>();
        }

        void Update()
        {
            Slerp();
        }


        void Slerp()
        {
            if (movingForward)
            {
                timer += Time.deltaTime * speed;
            }
            else
            {
                timer -= Time.deltaTime * speed;
            }


            this.transform.position = Vector3.Slerp(startPosition.position, endPosition.position, timer);


            if (movingForward)
            {
                if (timer >= 1f)
                {
                    timer = 1f;
                    movingForward = false;
                }
            }
            else
            {
                if (timer <= 0f)
                {
                    timer = 0f;
                    movingForward = true;
                }
            }

        }

        public Rigidbody GetRigidbody()
        {
            return myRigidbody;
        }
    }
}
