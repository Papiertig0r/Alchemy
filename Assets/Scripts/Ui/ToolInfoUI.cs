using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolInfoUI : MonoBehaviour
{
    public Text toolName;
    public Slider progressSlider;
    public ItemSlotUI itemSlotUi;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
