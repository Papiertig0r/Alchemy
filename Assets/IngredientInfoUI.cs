using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInfoUI : MonoBehaviour
{
    public Text currentPhase;
    public IngredientInfoSliderUi concentrationSlider;
    public IngredientInfoSliderUi puritySlider;
    public IngredientInfoSliderUi yieldSlider;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void SetIngredient(Ingredient ingredient)
    {
        currentPhase.text = ingredient.ingredientType.ToString();
        concentrationSlider.SetValue(ingredient.concentration);
        puritySlider.SetValue(ingredient.purity);
        yieldSlider.SetValue(ingredient.yield);
    }
}
