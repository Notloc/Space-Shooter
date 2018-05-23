namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "AI/Ship/Decisions/Finish Attack")]
    public class FinishAttackDecision : ShipDecision
    {
        [SerializeField] float attackLength = 5f;

        public override bool Decide(StateController controller)
        {
            if (controller.GetTimeInCurrentState() > attackLength)
            {
                return true;
            }
            return false;
        }
    }
}