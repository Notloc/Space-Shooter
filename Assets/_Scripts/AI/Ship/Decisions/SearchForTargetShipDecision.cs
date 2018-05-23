namespace SpaceAdventure.AI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "AI/Ship/Decisions/Search For Target")]
    public class SearchForTargetShipDecision : ShipDecision
    {
        public enum VisionType { LINE, SPHERE, CONE }

        [SerializeField] VisionType visionType;
        [SerializeField] LayerMask visionMask;
        [SerializeField] LayerMask targetMask;


        public override bool Decide(StateController controller)
        {
            ShipStateController shipController = controller as ShipStateController;

            RaycastHit hit;
            if (Look(shipController, out hit))
            {
                Debug.Log("Something is seen!");
            }

            //controller.HitToTarget();

            return false;

        }


        public bool Look(ShipStateController controller, out RaycastHit hit)
        {
            switch (visionType)
            {
                case VisionType.LINE:
                    return LineLook(controller, out hit);

                case VisionType.SPHERE:
                    return SphereLook(controller, out hit);

                case VisionType.CONE:
                    return ConeLook(controller, out hit);
            }
            return LineLook(controller, out hit);
        }

        private bool LineLook(ShipStateController controller, out RaycastHit hit)
        {
            Transform shipEyes = controller.GetEyes();


            Ray ray = new Ray(shipEyes.position, shipEyes.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * controller.GetVisionRange());

            return Physics.Raycast(ray, out hit, controller.GetVisionRange(), visionMask);
        }

        private bool SphereLook(ShipStateController controller, out RaycastHit hit)
        {
            hit = new RaycastHit();
            return false;
        }

        private bool ConeLook(ShipStateController controller, out RaycastHit hit)
        {
            hit = new RaycastHit();
            return false;
        }


        
    }
}