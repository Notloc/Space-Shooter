namespace SpaceAdventure
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Health : MonoBehaviour
    {
        [SerializeField] float currentHealth;
        [SerializeField] float maxHealth;

        [SerializeField] DeathAction[] deathActions;
        [SerializeField] bool dead = false;

        [Header("Interface: iMeter")]
        [SerializeField] MonoBehaviour healthMeter;

        private void Start()
        {
            UpdateHealthBar();
        }

        public void Damage(float amount)
        {
            if (dead)
            {
                return;
            }


            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Die();
            }

            UpdateHealthBar();
            
        }

        void Die()
        {
            if (dead)
            {
                return;
            }

            dead = true;

            DisableAllScripts();

            this.GetComponent<Rigidbody>().isKinematic = true;

            if (deathActions.Length > 0)
            {
                foreach (DeathAction deathAction in deathActions)
                {
                    deathAction.Perform(this.gameObject);
                }
            }

        }

        void UpdateHealthBar()
        {
            if (healthMeter != null)
            {
                (healthMeter as iMeter).UpdateValue(currentHealth / maxHealth);
            }
        }

        void DisableAllScripts()
        {
            MonoBehaviour[] scripts = this.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script != (this as MonoBehaviour))
                {
                    script.enabled = false;
                }
            }
        }

    }
}