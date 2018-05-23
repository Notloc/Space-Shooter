namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "AI/Ship/Actions/Chase")]
    public class Chase : ShipAction
    {
        public override void Perform(StateController controller)
        {
            ShipStateController shipStateController = controller as ShipStateController;
            ChaseTarget(shipStateController);
        }


        private void ChaseTarget(ShipStateController controller)
        {
            ShipMotor shipMotor = controller.GetMotor();
            Rigidbody ship = controller.GetRigidbody();
            Rigidbody target = controller.GetTarget();
            if (target == null)
            {
                return;
            }

            Steer(shipMotor, ship, target);

            AdjustThrottle();

            Vector3 toTarget = target.position - ship.position;

            float angle = Vector3.Angle(ship.transform.forward, toTarget);



            Vector3 velocityDifference = ship.velocity - target.velocity;

            //Check throttle
            //Check angle from player
            //Check velocity differences
            //Adjust throttle

            //steer based on stats/throttle
        }

        private void Steer(ShipMotor shipMotor, Rigidbody ship, Rigidbody target)
        {
            Vector3 toTarget = target.position - ship.position;

            Vector3 targetAngle = Quaternion.LookRotation(toTarget).eulerAngles;
            Vector3 shipAngles = ship.rotation.eulerAngles;

            //Yaw (Left/Right)
            float shipXAngle = shipAngles.x;
            float targetXAngle = targetAngle.x;

            if (shipXAngle > targetXAngle)
            {
                shipXAngle -= 360;
            }
            float clockwise = targetXAngle - shipXAngle;

            shipXAngle = shipAngles.x;
            if (targetXAngle > shipXAngle)
            {
                targetXAngle -= 360;
            }
            float counterClockwise = shipXAngle - targetXAngle;

            bool useClockwise = (clockwise <= counterClockwise);

            if (useClockwise)
            {
                shipMotor.Pitch(-1f * Time.fixedDeltaTime);
            }
            else
            {
                shipMotor.Pitch(1f * Time.fixedDeltaTime);
            }

            //Pitch (Up/Down)
            float shipYAngle = shipAngles.y;
            float targetYAngle = targetAngle.y;

            if (shipYAngle > targetYAngle)
            {
                shipYAngle -= 360;
            }
            clockwise = targetYAngle - shipYAngle;

            shipYAngle = shipAngles.y;
            if (targetYAngle > shipYAngle)
            {
                targetYAngle -= 360;
            }
            counterClockwise = shipYAngle - targetYAngle;

            useClockwise = (clockwise <= counterClockwise);

            if (useClockwise)
            {
                shipMotor.Yaw(-1f * Time.fixedDeltaTime);
            }
            else
            {
                shipMotor.Yaw(1f * Time.fixedDeltaTime);
            }

        }

        private void AdjustThrottle()
        {

        }

    }
}