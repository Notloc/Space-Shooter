namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "AI/Ship/Actions/Attack")]
    public class Attack : ShipAction
    {
        [SerializeField] float attackDelay;

        public override void Perform(StateController controller)
        {
            ShipStateController shipController = controller as ShipStateController;

            //Spin
            Spin(shipController);

            //Fire
            Fire(shipController);
        }


        void Spin(ShipStateController controller)
        {
            controller.GetMotor().Roll(1f);
        }

        void Fire(ShipStateController controller)
        {
            float timeInState = controller.GetTimeInCurrentState();

            if (timeInState > attackDelay)
            {
                Rigidbody shipRigidbody = controller.GetRigidbody();
                Ray forward = new Ray(shipRigidbody.position, shipRigidbody.transform.forward);

                controller.GetWeaponSystem().Fire(forward, controller.GetTarget());
            }
        }


    }
}