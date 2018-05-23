namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(ShipMotor))]
    public class PlayerShipController : MonoBehaviour
    {
        [SerializeField]
        float throttleSensitivity;

        [SerializeField]
        ShipMotor myMotor;

        [SerializeField]
        PlayerShipAimer playerAimer;

        [SerializeField]
        WeaponSystem weaponSystem;


        void Update()
        {
            MotorInput();
            WeaponSystemInput();
        }

        void MotorInput()
        {
            if (myMotor == null)
            {
                return;
            }

            TakeThrottleInput();
            TakeMovementInput();
            //TakeShieldInput();
        }
        void TakeThrottleInput()
        {
            if (Input.GetButton("Throttle Up"))
            {
                myMotor.IncreaseThrottle(Time.deltaTime * throttleSensitivity);
            }
            else if (Input.GetButton("Throttle Down"))
            {
                myMotor.DecreaseThrottle(Time.deltaTime * throttleSensitivity);
            }

            if (Input.GetButtonDown("Max Throttle"))
            {
                myMotor.IncreaseThrottle(10f);
            }
            else if (Input.GetButtonDown("Cut Throttle"))
            {
                myMotor.DecreaseThrottle(10f);
            }
        }
        void TakeMovementInput()
        {
            //Pitch
            myMotor.Pitch(Input.GetAxis("Pitch") * Time.deltaTime);

            //Yaw
            myMotor.Yaw(Input.GetAxis("Yaw") * Time.deltaTime);

            //Roll
            myMotor.Roll(Input.GetAxis("Roll") * Time.deltaTime);
        }

        void WeaponSystemInput()
        {
            if (weaponSystem == null || playerAimer == null)
            {
                return;
            }

            if (Input.GetButton("Fire Weapon"))
            {
                weaponSystem.Fire(playerAimer.GetAim(), playerAimer.GetTarget());
            }
        }
    }
}