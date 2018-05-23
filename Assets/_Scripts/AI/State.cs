namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class State : ScriptableObject
    {
        public abstract void UpdateState(StateController controller);
    }
}