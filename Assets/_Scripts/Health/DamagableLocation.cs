namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DamagableLocation : MonoBehaviour, iDamagable
    {
        [SerializeField]
        float health = 10f;

        [SerializeField]
        float damageMultiplier = 1f;

        public void Damage(float damage, RaycastHit hitInfo)
        {
            health -= damage * damageMultiplier;

            if (health <= 0)
            {
                Break();
            }

            Health healthPool = GetComponentInParent<Health>();
            if (healthPool)
            {
                healthPool.Damage(damage * damageMultiplier);
            }
        }

        void Break()
        {
            //print(this.name + " is now broken!");
        }

    }
}