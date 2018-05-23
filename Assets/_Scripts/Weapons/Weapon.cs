namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Weapon : MonoBehaviour
    {
        [Header("Weapon Stats")]
        [SerializeField]
        float projectileVelocity = 50f;
        [SerializeField]
        float fireRate;
        [SerializeField]
        float targetingMagnetism = 5f;
        [SerializeField]
        float magnetismTimeLimit = 2f;

        [Space]

        [Header("Targeting System")]
        [SerializeField]
        TargetingSystem targetingSystem;

        [Space]

        [Header("Projectile")]
        [SerializeField]
        Transform projectileSpawnPoint;
        [SerializeField]
        Projectile projectilePrefab;


        float fireRateTimer = 0f;

        protected virtual void Update()
        {
            ReduceTimer();
        }
        private void ReduceTimer()
        {
            fireRateTimer -= Time.deltaTime;
        }

        public virtual void Fire(Rigidbody myself, Vector3 targetPoint, Rigidbody target)
        {
            if (fireRateTimer > 0f)
            {
                return;
            }

            if (target && targetingSystem)
            {
                targetPoint = targetingSystem.Calculate(myself, target, projectileVelocity);
                TargetedShot(myself, target, targetPoint);
            }
            else
            {
                ManualShot(myself, targetPoint);
            }

        }

        private void TargetedShot(Rigidbody myself, Rigidbody target, Vector3 estimatedIntercept)
        {
            //Create projectile
            Projectile firedProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            //Calculate the velocity needed to compensate for ship movement
            //Calculate vector towards target and match magnitude to our velocity
            Vector3 compensationVelocity = (estimatedIntercept - projectileSpawnPoint.position).normalized;
            compensationVelocity *= myself.velocity.magnitude;

            //Remove our ships velocity from it to get our final compensation velocity
            compensationVelocity -= myself.velocity;

            float velocityUsedToCompensate = compensationVelocity.magnitude;

            firedProjectile.transform.LookAt(estimatedIntercept);

            firedProjectile.Fire(myself.velocity +
                                      compensationVelocity +
                                        (firedProjectile.transform.forward *
                                          (projectileVelocity - velocityUsedToCompensate)),

                                    target,
                                    targetingMagnetism,
                                    magnetismTimeLimit);

            fireRateTimer = 1f / fireRate;
        }

        private void ManualShot(Rigidbody myself, Vector3 targetPoint)
        {
            Projectile firedProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            firedProjectile.transform.LookAt(targetPoint);

            firedProjectile.Fire(myself.velocity + (firedProjectile.transform.forward * projectileVelocity));

            fireRateTimer = 1f / fireRate;
        }
    }
}