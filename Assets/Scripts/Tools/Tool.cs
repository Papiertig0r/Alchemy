using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public Ingredient ingredient;
    public IngredientType ingredientType;
    public ToolType toolType;
    public float efficiency;

    public bool AddIngredient(Ingredient ingredient)
    {
        PhaseTransition phTr = FindSuitablePhaseTransition(ingredient);
        if(phTr == null)
        {
            return false;
        }

        if(this.ingredient != null)
        {
            PlayerController.player.inventory.AddItem(ingredient.GetComponent<Item>());
        }

        this.ingredient = ingredient;
        this.ingredient.currentProgress = 0f;
        this.ingredient.currentToolType = this.toolType;
        return true;
    }

    private PhaseTransition FindSuitablePhaseTransition(Ingredient ingredient)
    {
        // Find all phase transitions using this tool type
        List<PhaseTransition> phaseTransitions = ingredient.phaseTransition.FindAll(phase => phase.toolType == this.toolType);
        PhaseTransition phaseTransition = phaseTransitions.Find(phase => phase.originalType == ingredient.ingredientType);

        return phaseTransition;
    }

    private void Update()
    {
        if(ingredient != null)
        {
            ProcessIngredient();
        }
    }

    private void ProcessIngredient()
    {
        ingredient.currentProgress += Time.deltaTime * TimeManager.ticksPerSecond;
        ingredient.ChangeConcentration(Time.deltaTime * TimeManager.ticksPerSecond * efficiency);

        if (ingredient.currentProgress >= Ingredient.maxProgress)
        {
            ingredient.currentProgress = Ingredient.minProgress;
            PhaseTransition phTr = FindSuitablePhaseTransition(ingredient);
            ingredient.ingredientType = phTr.endType;
            ingredient.name = phTr.endProductName;
        }
    }
}
