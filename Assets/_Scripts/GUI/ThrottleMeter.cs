namespace SpaceAdventure.GUI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ThrottleMeter : MonoBehaviour, iMeter
    {
        Image myMeter;

        [SerializeField]
        float maxHeight;

        void Start()
        {
            myMeter = this.GetComponent<Image>();
        }

        public void UpdateValue(float value)
        {
            //Clamp value to [0, 1]
            value = Mathf.Clamp(value, 0.0f, 1.0f);

            //Calculate new height
            float newHeight = value * maxHeight;

            //Setup a Vector2
            Vector2 newSize = myMeter.rectTransform.sizeDelta;
            newSize.y = newHeight;

            //Set meter height with the Vector2
            myMeter.rectTransform.sizeDelta = newSize;
        }
    }
}