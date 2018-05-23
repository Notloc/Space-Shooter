namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class WeaponSystem : MonoBehaviour
    {

        [SerializeField]
        Rigidbody myRigidbody;

        [SerializeField]
        Weapon[] weapons;

        [SerializeField]
        float zeroingDistance = 100f;

        [SerializeField]
        LayerMask aimRayCastMask;

        public void Fire(Ray aim)
        {
            PrepareToFire(aim, null);
        }

        public void Fire(Ray aim, Rigidbody target)
        {
            PrepareToFire(aim, target);
        }


        void PrepareToFire(Ray aim, Rigidbody target)
        {
            Vector3 aimPoint;

            RaycastHit hit;
            if (Physics.Raycast(aim, out hit, zeroingDistance, aimRayCastMask))
            {
                aimPoint = hit.point;
            }
            else
            {
                aimPoint = aim.origin + (aim.direction.normalized * zeroingDistance);
            }
            FireWeapons(aimPoint, target);
        }

        void FireWeapons(Vector3 aimPoint, Rigidbody target)
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.Fire(myRigidbody, aimPoint, target);
            }
        }

    }
}