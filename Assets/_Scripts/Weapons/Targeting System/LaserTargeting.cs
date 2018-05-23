namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LaserTargeting : TargetingSystem
    {
        public override Vector3 Calculate(Rigidbody shooter, Rigidbody target, float projectileVelocity)
        {
            //Quickly aproximate a target point
            //Projectile magnetism will do the rest


            Vector3 speedDifference = target.velocity - shooter.velocity;

            //How much speed the projectile has to move towards the target after canceling the speed difference
            float remainingProjectileSpeed = projectileVelocity - speedDifference.magnitude;

            //If we can't make the shot
            if (remainingProjectileSpeed <= 0f)
            {
                return Vector3.positiveInfinity;
            }


            float distance = Vector3.Distance(shooter.position, target.position);

            //How long it will aproximately take the projectile
            float time = distance / remainingProjectileSpeed;

            //Aproximate our target point
            Vector3 targetPoint = target.position + target.velocity * time;

            return targetPoint;
        }
    }
}