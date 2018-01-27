using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableInfoUi : MonoBehaviour
{
    public Text descriptionText;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void SetConsumable(Consumable consumable)
    {
        descriptionText.text = consumable.description;
    }
}
