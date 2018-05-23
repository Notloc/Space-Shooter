namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class ShipMotor : MonoBehaviour
    {

        Rigidbody myRigidbody;

        [Header("Ship Stats")]
        [Header("  Curves are in degrees per second")]
        [SerializeField]
        AnimationCurve pitchHandlingCurve;
        [SerializeField]
        AnimationCurve yawHandlingCurve;
        [SerializeField]
        AnimationCurve rollHandlingCurve;

        [Space]

        [SerializeField]
        AnimationCurve dragCurve;

        [Space]
        [Space]

        [SerializeField]
        float maxThrust;
        [SerializeField]
        float minThrust;

        [Space]

        [Header("Internal Values")]
        [SerializeField]
        float throttle;
        [SerializeField]
        bool invertPitch = false;

        [Header("Interfaces: iMeters")]
        [SerializeField]
        GameObject throttleMeter;

        [Space]

        [Header("Dev Variables")]
        [SerializeField]
        Vector3 myVelocity;
        [SerializeField]
        float myMagnitude;

        bool braking = false;

        void Start()
        {
            myRigidbody = this.GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            AdjustDrag();
            ApplyThrust();
        }

        //Adjust drag based on thorttle to create a better feel
        void AdjustDrag()
        {
            if (braking)
            {
                myRigidbody.drag = dragCurve.Evaluate(0.75f);
                braking = false;
            }
            else
            {
                myRigidbody.drag = dragCurve.Evaluate(throttle);
            }

        }

        void ApplyThrust()
        {
            float variableThrust = maxThrust - minThrust;

            Vector3 thrust = (Vector3.forward * variableThrust * throttle) + (Vector3.forward * minThrust);
            myRigidbody.AddRelativeForce(thrust);

            //DEV
            myVelocity = myRigidbody.velocity;
            myMagnitude = myVelocity.magnitude;
        }


        //Rotation Methods
        public void Pitch(float amount)
        {
            float invert = 1f;
            if (invertPitch)
            {
                invert = -1f;
            }

            float pitchHandling = pitchHandlingCurve.Evaluate(throttle);
            myRigidbody.rotation = myRigidbody.rotation * Quaternion.Euler(amount * invert * pitchHandling, 0f, 0f);
        }
        public void Yaw(float amount)
        {
            float yawHandling = yawHandlingCurve.Evaluate(throttle);
            myRigidbody.rotation = myRigidbody.rotation * Quaternion.Euler(0f, amount * yawHandling, 0f);
        }
        public void Roll(float amount)
        {
            float rollHandling = rollHandlingCurve.Evaluate(throttle);
            myRigidbody.rotation = myRigidbody.rotation * Quaternion.Euler(0f, 0f, amount * rollHandling);
        }

        //Throttle Methods
        public void IncreaseThrottle(float amount)
        {
            throttle = Mathf.Clamp(throttle + amount, 0.0f, 1.0f);
            UpdateThrottletMeter();
        }
        public void DecreaseThrottle(float amount)
        {
            //Brake if we are already at 0 throttle
            if (throttle == 0)
            {
                braking = true;
            }
            else
            {
                throttle = Mathf.Clamp(throttle - amount, 0.0f, 1.0f);
                UpdateThrottletMeter();
            }

        }

        void UpdateThrottletMeter()
        {
            if (throttleMeter == null)
            {
                return;
            }

            iMeter theMeter = throttleMeter.GetComponent<iMeter>();

            //If this really is a meter
            if (theMeter != null)
            {
                theMeter.UpdateValue(throttle);
            }
            else
            {
                Debug.LogError(this.name + " - ShipMotor: throttletMeter does not have an iMeter script attached.");
            }
        }
    }
}