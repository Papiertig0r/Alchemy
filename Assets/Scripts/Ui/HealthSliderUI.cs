using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderUI : MonoBehaviour
{
    private Slider slider;
    // Use this for initialization
    void Start ()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateHealth(Stat health)
    {
        slider.minValue = health.min;
        slider.maxValue = health.max;
        slider.value = health.current;
        slider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, new Color(0f, 0.8f, 0f), health.current / health.max);
    }
}
