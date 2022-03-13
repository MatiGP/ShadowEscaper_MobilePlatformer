using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class ColorModifier : MonoBehaviour
    {
        [SerializeField] Image[] images;

        public void SetColorByHex(string hex)
        {
            Color c;
            ColorUtility.TryParseHtmlString(hex, out c);

            foreach (Image image in images)
            {
                image.color = c;
            }

            SaveSystem.SaveColorData(hex);
        }
    }
}
