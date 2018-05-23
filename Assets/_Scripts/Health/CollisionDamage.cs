namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CollisionDamage : MonoBehaviour
    {

        [SerializeField] Health healthPool;

        [SerializeField] float minimumSpeedForDamage;

        [SerializeField] float speedForMaxDamage;
        [SerializeField] float maximumDamage;

        private void OnCollisionEnter(Collision collision)
        {
            //This implementation is weak.
            //Colliding with a pebble is the same as colliding with a wall

            float collisionSpeed = collision.relativeVelocity.magnitude;

            if (collisionSpeed > minimumSpeedForDamage)
            {   
                //      damage =   ( speed past threshold              /      amount past threshold for max damage )       * max damage   
                float damage = ((collisionSpeed - minimumSpeedForDamage) / (speedForMaxDamage - minimumSpeedForDamage)) * maximumDamage;
                healthPool.Damage(damage);
            }

        }

    }
}