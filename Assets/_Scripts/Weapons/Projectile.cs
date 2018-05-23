namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        float damage;

        [SerializeField]
        GameObject hitEffect;

        [SerializeField]
        LayerMask hitMask;

        [Space]


        Rigidbody myRigidbody;
        Rigidbody myTarget;
        Vector3 myVelocity;

        //Projectile Magnetism
        float myMagnetism = 0f;
        float magnetismTimeLeft = 1.5f;

        protected virtual void Start()
        {
            myRigidbody = this.GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            DoMagnetism();
            MoveProjectile();
            OrientateProjectile();
        }

        protected virtual void MoveProjectile()
        {
            Ray ray = new Ray(myRigidbody.position, myVelocity);

            //Try to damage something
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, (myVelocity.magnitude * Time.fixedDeltaTime), hitMask))
            {
                iDamagable target = hit.collider.GetComponentInParent<iDamagable>();
                if (target != null)
                {
                    //Deal damage and spawn an effect if one is assigned
                    target.Damage(damage, hit);

                }

                if (hitEffect)
                {
                    Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }

                Destroy(this.gameObject);
            }

            //Move if we don't collide with anything
            else
            {
                myRigidbody.MovePosition(myRigidbody.position + (myVelocity * Time.fixedDeltaTime));
            }
        }

        void DoMagnetism()
        {
            //If we have a target
            if (myTarget)
            {
                //Clear target if we miss the shot
                //This way the projectile doesn't loop around
                Vector3 distanceFromTarget = myTarget.position - myRigidbody.position;
                if (90f <= Vector3.Angle(myVelocity, distanceFromTarget) || magnetismTimeLeft <= 0)
                {
                    myTarget = null;
                }
                //Otherwise, apply magnestism
                else
                {
                    //Rotate towards the target
                    Vector3 directionTowardsTarget = myTarget.position - myRigidbody.position;
                    float rotationInRadians = (myMagnetism / 180f) * Mathf.PI;

                    Vector3 rotatedVelocity = Vector3.RotateTowards(myVelocity, directionTowardsTarget, rotationInRadians, float.PositiveInfinity);

                    //Update velocity, but maintain magnitude
                    myVelocity = rotatedVelocity.normalized * myVelocity.magnitude;
                }

                magnetismTimeLeft -= Time.fixedDeltaTime;
            }
        }

        protected virtual void OrientateProjectile()
        {
            //Face the way we are moving
            myRigidbody.rotation = Quaternion.LookRotation(myVelocity, Vector3.up);
        }

        //Fire for manual shots
        public virtual void Fire(Vector3 velocity)
        {
            myVelocity = velocity;
        }
        //Fire for shots with targeting
        public virtual void Fire(Vector3 velocity, Rigidbody target, float magnetism, float magnetismTime)
        {
            myVelocity = velocity;
            myTarget = target;
            myMagnetism = magnetism;
            magnetismTimeLeft = magnetismTime;
        }
    }
}