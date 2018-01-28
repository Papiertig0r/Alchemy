using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Applyable, IConsumable, IShowable
{
    public static float maxProgress = 100f;
    public static float minProgress = 0f;

    public float concentration = 0f;
    public float purity = 0f;
    public float yield = 0f;

    //! would current phase be better?
    public IngredientType ingredientType;
    public ToolType currentToolType = ToolType.NONE;
    public float currentProgress = 0f;

    public List<PhaseTransition> phaseTransition = new List<PhaseTransition>();

    public List<Effect> effectList = new List<Effect>();

    public void Awake()
    {
    }

    public void Transition()
    {

    }

    public override void Apply(CharaController target)
    {
        foreach(Effect effect in effectList)
        {
            effect.Apply(target, concentration, purity, yield, ingredientType);
        }
        Destroy(this.gameObject);
    }

    public void ChangeConcentration(float change)
    {
        concentration += change;
        concentration = Mathf.Clamp(concentration, 0f, 100f);
    }

    public void ChangeProgress(float change)
    {

    }

    public void Consume(CharaController target)
    {
        Apply(target);
    }

    public void DeactivateExtendedInfo()
    {
        UIManager.ingredientInfoUi.Deactivate();
    }

    public void ShowExtendedInfo()
    {
        UIManager.ingredientInfoUi.SetIngredient(this);
        UIManager.ingredientInfoUi.Activate();
    }
}
