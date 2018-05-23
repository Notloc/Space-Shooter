namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ShipStateController : StateController
    {
        [SerializeField] ShipMotor shipMotor;
        [SerializeField] Rigidbody shipRigidbody;

        [SerializeField] WeaponSystem weaponSystem;

        void FixedUpdate()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (currentState == null)
            {
                Debug.LogError("Error: " + this.gameObject.name + "'s AI does not have an assigned state.");
                return;
            }

            currentState.UpdateState(this);
        }

        public ShipMotor GetMotor()
        {
            return shipMotor;
        }

        public Rigidbody GetRigidbody()
        {
            return shipRigidbody;
        }

        public WeaponSystem GetWeaponSystem()
        {
            return weaponSystem;
        }
    }
}