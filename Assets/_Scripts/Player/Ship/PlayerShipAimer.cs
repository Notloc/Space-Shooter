namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using System.Linq;

    public class PlayerShipAimer : MonoBehaviour, iProvideTarget, iProvideAim
    {

        [Header("Screen Camera")]
        [SerializeField]
        Camera myCamera;

        [Space]

        [Header("Angle Limit")]
        [SerializeField]
        float aimAngleRange;

        [Space]

        [Header("GUI")]
        [SerializeField]
        RectTransform canvas;
        [SerializeField]
        RectTransform crosshair;
        [SerializeField]
        RectTransform targetReticle;

        [Space]

        [Header("Sphere Cast Settings")]
        [SerializeField]
        float sphereCastMinRadius = 5f;
        [SerializeField]
        float sphereCastMaxRadius = 0.5f;
        [SerializeField]
        float sphereCastDistance = 1000f;
        [SerializeField]
        LayerMask targetableMask;

        [Space]

        [Header("Targeting")]
        [SerializeField]
        bool lockedOn = false;
        [SerializeField]
        Rigidbody targetRigidbody;
        [SerializeField]
        Rigidbody potentialTarget;

        [SerializeField]
        float timeToLockOn = 5f;

        Rigidbody previousTarget;
        Ray aimRay;

        float lockOnTimer;



        void Update()
        {
            UpdateLockOn();
            UpdateGUI();
        }

        void UpdateLockOn()
        {

            //if locked
            if (lockedOn)
            {
                if (targetRigidbody == null || !Input.GetButton("Lock On"))
                {
                    lockedOn = false;
                    targetRigidbody = null;
                    lockOnTimer = timeToLockOn;
                    return;
                }

                Vector3 towardsTarget = targetRigidbody.position - this.transform.position;
                if (Vector3.Angle(this.transform.forward, towardsTarget) > 1.5f * aimAngleRange)
                {
                    lockedOn = false;
                    targetRigidbody = null;
                    lockOnTimer = timeToLockOn;
                    return;
                }

            }
            //if not locked
            else
            {
                if (previousTarget != potentialTarget)
                {
                    lockOnTimer = timeToLockOn;
                }

                if (Input.GetButton("Lock On"))
                {
                    lockOnTimer -= Time.deltaTime;
                }
                else
                {
                    lockOnTimer = timeToLockOn;
                }

                if (lockOnTimer <= 0f)
                {
                    lockedOn = true;
                    targetRigidbody = potentialTarget;
                }
            }
        }

        void UpdateGUI()
        {
            //Update crosshair position
            crosshair.transform.position = Input.mousePosition;

            //If we have a target, put the target reticle on them
            if (potentialTarget != null)
            {
                Vector3 screenPos = Camera.main.WorldToViewportPoint(potentialTarget.position);
                screenPos.x *= canvas.rect.width;
                screenPos.y *= canvas.rect.height;

                targetReticle.gameObject.SetActive(true);
                targetReticle.position = screenPos;
            }
            else
            {
                targetReticle.gameObject.SetActive(false);
            }
        }

        void FixedUpdate()
        {
            //Determine ray to use for aiming
            aimRay = UpdateAim();
            aimRay = ValidateAim(aimRay);


            UpdatePotentialTarget();
        }

        Ray UpdateAim()
        {
            return myCamera.ScreenPointToRay(Input.mousePosition);
        }
        Ray ValidateAim(Ray aim)
        {
            float angle = Vector3.Angle(this.transform.forward, aim.direction);

            //Correct aim if out of bounds
            if (angle > aimAngleRange)
            {
                //Calculate required correction
                angle = angle - aimAngleRange;
                float radianCorrection = angle * (Mathf.PI / 180f);

                aim.direction = Vector3.RotateTowards(aim.direction, this.transform.forward, radianCorrection, 1f);
            }

            return aim;
        }

        void UpdatePotentialTarget()
        {
            if (!lockedOn)
            {
                previousTarget = potentialTarget;

                //Get a new potential target
                potentialTarget = PerformCastForTarget(aimRay);
            }
        }
        Rigidbody PerformCastForTarget(Ray ray)
        {
            //Raycast for objects and order by distance
            RaycastHit[] hits = Physics.SphereCastAll(ray, sphereCastMaxRadius, sphereCastDistance, targetableMask).OrderBy(h => h.distance).ToArray(); ;

            //If we hit anything
            if (hits.Length > 0)
            {

                foreach (RaycastHit hit in hits)
                {
                    //Find objects that are targetable
                    iTargetable target = hit.collider.GetComponentInParent<iTargetable>();
                    if (target != null)
                    {
                        //Simulate the sphereCast growing in size with distance
                        //This is to account for foreshortening
                        if (InRange(hit, ray))
                        {
                            //If we determine the target is in range
                            return target.GetRigidbody();
                        }
                    }
                }


            }
            return null;
        }
        bool InRange(RaycastHit hit, Ray ray)
        {
            Vector3 hitPosition = hit.collider.transform.position;

            float distance = Vector3.Distance(myCamera.transform.position, hitPosition);

            //Roughly the point on the ray where the spherecast hit
            Vector3 relativePointOnRay = ray.origin + (ray.direction * distance);

            float effectiveRadius = Mathf.Lerp(sphereCastMinRadius, sphereCastMaxRadius, (distance / sphereCastDistance));


            if (Vector3.Distance(relativePointOnRay, hitPosition) <= effectiveRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //INTERFACES

        //Provide Target
        public Rigidbody GetTarget()
        {
            return targetRigidbody;
        }

        //Provide Aim
        public Ray GetAim()
        {
            return aimRay;
        }
    }
}