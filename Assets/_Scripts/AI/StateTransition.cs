namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class StateTransition
    {
        [SerializeField] Decision decision;
        [SerializeField] State successState;
        [SerializeField] State failState;

        public bool Decide(StateController controller)
        {
            return decision.Decide(controller);
        }

        public State SuccessState()
        {
            return successState;
        }

        public State FailState()
        {
            return failState;
        }

    }
}
