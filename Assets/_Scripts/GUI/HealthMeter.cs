namespace SpaceAdventure.GUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HealthMeter : MonoBehaviour, iMeter
    {

        [SerializeField] float maxBarWidth;

        [SerializeField] RectTransform healthBar;

        public void UpdateValue(float value)
        {
            Vector2 newSize = healthBar.sizeDelta;
            newSize.x = maxBarWidth * value;

            healthBar.sizeDelta = newSize;
        }

    }
}