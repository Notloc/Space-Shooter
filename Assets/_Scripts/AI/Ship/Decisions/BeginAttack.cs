namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "AI/Ship/Decisions/Begin Attack")]
    public class BeginAttack : ShipDecision
    {
        [SerializeField] float maxRange;
        [SerializeField] float maxAngle;

        [SerializeField] float stateDelay;

        public override bool Decide(StateController controller)
        {
            ShipStateController shipController = controller as ShipStateController;

            //False if we haven't been in the current state long enough
            if (shipController.GetTimeInCurrentState() < stateDelay)
            {
                return false;
            }

            Rigidbody target = controller.GetTarget();
            if (target == null)
            {
                return false;
            }

            return ValidatePosition(shipController, target);
        }

        bool ValidatePosition(ShipStateController controller, Rigidbody target)
        {
            Rigidbody myself = controller.GetRigidbody();

            //False if too far away
            if (Vector3.Distance(myself.position, target.position) > maxRange)
            {
                return false;
            }

            //False if we aren't facing near the target
            Vector3 towards = target.position - myself.position;
            if (Vector3.Angle(myself.transform.forward, towards) > maxAngle)
            {
                return false;
            }

            return true;
        }

    }
}