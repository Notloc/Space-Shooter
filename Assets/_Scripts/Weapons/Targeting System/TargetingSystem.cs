namespace SpaceAdventure
{
    using UnityEngine;

    public abstract class TargetingSystem : MonoBehaviour
    {
        public abstract Vector3 Calculate(Rigidbody shooter, Rigidbody target, float velocity);
    }
}