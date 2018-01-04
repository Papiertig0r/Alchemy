using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public Ingredient ingredient;
    public IngredientType ingredientType;
    public ToolType toolType;
    private bool toolCanBeUsed = false;

    public bool AddIngredient(Ingredient ingredient)
    {
        Debug.Log(ingredient.name);
        // Find all phase transitions using this tool type
        List<PhaseTransition> phaseTransitions = ingredient.phaseTransition.FindAll(phase => phase.toolType == this.toolType);
        Debug.Log(phaseTransitions.Count);
        PhaseTransition phaseTransition = phaseTransitions.Find(phase => phase.originalType == ingredient.ingredientType);
        Debug.Log(phaseTransition);
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
        return true;
    }

    private void Update()
    {
        if(toolCanBeUsed && Input.GetButtonDown("Fire1"))
        {
            // Handles Input
        }

        if(ingredient != null)
        {
            // Handles ingredient cooking
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<PlayerController>())
        {
            toolCanBeUsed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<PlayerController>())
        {
            toolCanBeUsed = false;
        }
    }
}
