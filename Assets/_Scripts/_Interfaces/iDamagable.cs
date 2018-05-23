namespace SpaceAdventure
{
    using UnityEngine;
    public interface iDamagable
    {
        void Damage(float damage, RaycastHit hitInfo);
    }
}