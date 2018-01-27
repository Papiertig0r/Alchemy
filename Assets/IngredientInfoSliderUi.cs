using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInfoSliderUi : MonoBehaviour
{
    public Slider slider;
    public Text text;

    public void SetValue(float value)
    {
        slider.value = value;
        text.text = value.ToString("F0");
    }
}
