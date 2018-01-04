using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    public static float ticksPerSecond = 10f;
    public ItemSlotUI itemSlotUI;
    public Slider progressSlider;

    private void Update()
    {
        progressSlider.value += Time.deltaTime * ticksPerSecond;
        if(progressSlider.value >= progressSlider.maxValue)
        {
            progressSlider.value = progressSlider.minValue;
        }
    }
}
