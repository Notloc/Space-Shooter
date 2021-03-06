﻿namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Action : ScriptableObject
    {
        public abstract void Perform(StateController controller);
    }
}