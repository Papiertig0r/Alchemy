using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    public ItemSlotUI itemSlotUI;
    public Slider progressSlider;

    public void UpdateSlider(float newValue)
    {
        progressSlider.value = newValue;
    }

    public void SetActive(bool newActive)
    {
        gameObject.SetActive(newActive);
    }

    public bool active
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    public void AddItem()
    {

    }

    public void RemoveItem()
    {

    }
}
