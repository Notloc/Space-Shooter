namespace SpaceAdventure
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Target : MonoBehaviour, iTargetable
    {



        public Rigidbody GetRigidbody()
        {
            return this.GetComponent<Rigidbody>();
        }

    }
}