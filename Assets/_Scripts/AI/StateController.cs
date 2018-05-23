namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StateController : MonoBehaviour
    {
        [Header("State")]
        [SerializeField] protected State currentState;

        [Header("Vision")]
        [SerializeField] Transform eyes;
        [SerializeField] protected float visionRange;

        [Header("Target")]
        [SerializeField] Rigidbody target;

        float timeInCurrentState = 0f;

        private void Update()
        {
            IncreaseTimer();
        }

        void IncreaseTimer()
        {
            if (currentState != null)
            {
                timeInCurrentState += Time.deltaTime;
            }
            else
            {
                timeInCurrentState = 0f;
            }
        }

        public float GetTimeInCurrentState()
        {
            return timeInCurrentState;
        }

        public void SetNewState(State newState)
        {
            currentState = newState;
            timeInCurrentState = 0f;
        }

        public Transform GetEyes()
        {
            return eyes;
        }

        public float GetVisionRange()
        {
            return visionRange;
        }

        public void SetTarget(Rigidbody newTarget)
        {
            target = newTarget;
        }
        public Rigidbody GetTarget()
        {
            return target;
        }

    }
}