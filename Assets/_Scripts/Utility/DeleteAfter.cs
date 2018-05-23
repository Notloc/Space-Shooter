namespace Utility
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DeleteAfter : MonoBehaviour
    {

        [SerializeField]
        float timeBeforeDeletion = 10f;

        private void Start()
        {
            StartCoroutine(DelayedDeletion(timeBeforeDeletion));
        }

        IEnumerator DelayedDeletion(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(this.gameObject);
        }

    }
}