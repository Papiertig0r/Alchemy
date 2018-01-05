using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public ToolUI toolUI;
    public Ingredient ingredient;
    public IngredientType ingredientType;
    public ToolType toolType;

    private bool toolCanBeUsed = false;

    public bool AddIngredient(Ingredient ingredient)
    {
        // Find all phase transitions using this tool type
        List<PhaseTransition> phaseTransitions = ingredient.phaseTransition.FindAll(phase => phase.toolType == this.toolType);
        PhaseTransition phaseTransition = phaseTransitions.Find(phase => phase.originalType == ingredient.ingredientType);

        if (
            phaseTransitions.Count == 0 ||
            phaseTransition == null
            )
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

        if(ingredient.currentProgress >= Ingredient.maxProgress)
        {
            ingredient.currentProgress = Ingredient.minProgress;
        }

        toolUI.UpdateSlider(ingredient.currentProgress);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            if(player.inventory.isUiActive)
            {
                toolUI.SetActive(true);
            }
            player.onInventoryDown += Display;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if (player)
        {
            toolUI.SetActive(false);
            player.onInventoryDown -= Display;
        }
    }

    private void Display()
    {
        Inventory inventory = PlayerController.player.inventory;
        bool isUiActive = !inventory.isUiActive;
        inventory.Display(isUiActive);
        toolUI.SetActive(isUiActive);
    }
}
