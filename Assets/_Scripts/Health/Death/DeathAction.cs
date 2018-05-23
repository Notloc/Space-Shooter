using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeathAction : ScriptableObject
{
    public abstract void Perform(GameObject theDying);
}
