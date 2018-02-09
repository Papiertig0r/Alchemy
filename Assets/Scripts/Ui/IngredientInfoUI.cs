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
    public Transform panel;

    public Text textPrefab;

    [SerializeField]
    private List<Text> textList = new List<Text>();

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

        ManageTexts(ingredient);

        VerticalLayoutGroup vlg = panel.GetComponent<VerticalLayoutGroup>();
        float textHeight = textPrefab.rectTransform.sizeDelta.y;
        int numberOfEffects = ingredient.effectList.Count;
        float height = vlg.padding.top + vlg.padding.bottom + vlg.spacing * (numberOfEffects - 1) + textHeight * numberOfEffects;

        RectTransform panelRect = panel.GetComponent<RectTransform>();
        Vector2 sizeDelta = panelRect.sizeDelta;
        sizeDelta.y = height;
        panelRect.sizeDelta = sizeDelta;
        sizeDelta.y += Mathf.Abs(panelRect.anchoredPosition.y);
        GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    private void ManageTexts(Ingredient ingredient)
    {
        int numberOfEffects = ingredient.effectList.Count;
        if (numberOfEffects > textList.Count)
        {
            int diff = numberOfEffects - textList.Count;
            for (; diff > 0; diff--)
            {
                textList.Add(Instantiate(textPrefab, panel));
            }
        }

        for (int i = 0; i < textList.Count; i++)
        {
            bool isActive = i < numberOfEffects;
            if (i < numberOfEffects)
            {
                textList[i].text = ingredient.effectList[i].ToString(ingredient);
            }
            textList[i].gameObject.SetActive(isActive);
        }
    }
}
