namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "AI/Ship/State")]
    public class ShipState : State
    {
        [SerializeField] ShipAction[] actions;
        [SerializeField] StateTransition[] transitions;

        public override void UpdateState(StateController controller)
        {
            if (!(controller is ShipStateController))
            {
                Debug.LogError("Error: ShipAIStateController expected.");
                return;
            }

            PerformActions(controller);

            PerformStateTransitions(controller);
        }

        private void PerformActions(StateController controller)
        {
            for(int i = 0; i < actions.Length; i++)
            {
                actions[i].Perform(controller);
            }
        }

        private void PerformStateTransitions(StateController controller)
        {
            for(int i = 0; i < transitions.Length; i++)
            {
                bool outcome = transitions[i].Decide(controller);

                if (outcome == true)
                {
                    State newState = transitions[i].SuccessState();
                    if (newState)
                    {
                        controller.SetNewState(newState);
                    }
                    
                }
                else
                {
                    State newState = transitions[i].FailState();
                    if (newState)
                    {
                        controller.SetNewState(newState);
                    }
                }

            }
        }

    }
}