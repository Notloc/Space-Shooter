using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Death Actions/Report Death")]
public class ReportDeath : DeathAction
{

    public override void Perform(GameObject theDying)
    {
        WinScript winScript = FindObjectOfType<WinScript>();
        winScript.Kill(theDying.GetComponent<Rigidbody>());
    }
}
