namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Death Effects/Player Death")]
    public class PlayerDeath : DeathAction
    {
        [SerializeField] GameObject explosionEffect;

        [SerializeField] GameObject returnToMenuPackage;

        public override void Perform(GameObject theDying)
        {
            Instantiate(explosionEffect, theDying.transform.position, Quaternion.identity);

            Instantiate(returnToMenuPackage, Vector3.zero, Quaternion.identity);

            Destroy(theDying);
        }
    }
}