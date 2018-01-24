using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public Ingredient ingredient;
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
        PhaseTransition phaseTransition = ingredient.phaseTransition.Find(phase => phase.toolType == this.toolType);

        return phaseTransition;
    }

    private void Update()
    {
        if(ingredient != null)
        {
            //ProcessIngredient();
        }
    }

    private void ProcessIngredient()
    {
        ingredient.currentProgress += Time.deltaTime * TimeManager.ticksPerSecond;
        ingredient.ChangeConcentration(Time.deltaTime * TimeManager.ticksPerSecond * efficiency);

        if (ingredient.currentProgress >= Ingredient.maxProgress)
        {
            ingredient.currentProgress = Ingredient.minProgress;
            AdvanceIngredient();
        }
    }

    public void AdvanceIngredient()
    {
        PhaseTransition phTr = FindSuitablePhaseTransition(ingredient);
        if (phTr != null)
        {
            Ingredient temp = Instantiate(phTr.endProduct, ingredient.transform.position, ingredient.transform.rotation);
            Destroy(ingredient.gameObject);
            ingredient = temp;
        }
    }
}
