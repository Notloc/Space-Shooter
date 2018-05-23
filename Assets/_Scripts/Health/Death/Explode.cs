using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Death Effects/Explosion")]
public class Explode : DeathAction
{
    [SerializeField] GameObject explosionEffect;

    public override void Perform(GameObject theDying)
    {
        Instantiate(explosionEffect, theDying.transform.position, Quaternion.identity);
        Destroy(theDying);
    }
}
