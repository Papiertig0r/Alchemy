using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Interactable
{
    public new string name;
    public Ingredient ingredient;
    public ToolType toolType;
    public float efficiency;

    private bool isProcessable = false;

    public bool AddIngredient(Ingredient ingredient)
    {
        PhaseTransition phTr = FindSuitablePhaseTransition(ingredient);
        if(phTr == null)
        {
            return false;
        }

        this.ingredient = ingredient;
        this.ingredient.currentProgress = 0f;
        this.ingredient.currentToolType = this.toolType;

        UIManager.toolInfoUi.UpdateIngredient();
        isProcessable = true;
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
        if(ingredient != null && isProcessable)
        {
            ProcessIngredient();
            UpdateUi();
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
            Ingredient temp = Instantiate(phTr.endProduct, ingredient.transform.position, ingredient.transform.rotation, this.transform);
            temp.concentration = ingredient.concentration;
            temp.purity = ingredient.purity;
            temp.yield = ingredient.yield;
            //temp.effectList = ingredient.effectList;

            Destroy(ingredient.gameObject);

            temp.gameObject.SetActive(false);
            ingredient = temp;

            phTr = FindSuitablePhaseTransition(ingredient);
            if (phTr == null)
            {
                isProcessable = false;
            }

            UIManager.toolInfoUi.UpdateIngredient();
        }
    }

    public override void Interact()
    {
        Debug.Log("Interact");
        if (UIManager.toolInfoUi.IsActive())
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    private void Activate()
    {
        StateController.instance.toolUiAction += HandleItemSwitching;
        UIManager.toolInfoUi.SetTool(this);
        UIManager.toolInfoUi.Activate();
        UpdateUi();
    }

    private void Deactivate()
    {
        StateController.instance.toolUiAction -= HandleItemSwitching;
        UIManager.toolInfoUi.Deactivate();
    }

    protected override void Left()
    {
        Deactivate();
    }

    //! \todo switched items should have the same slot
    public void HandleItemSwitching()
    {
        if(ingredient != null)
        {
            if(PlayerController.player.inventory.AddItem(ingredient.GetComponent<Item>()))
            {
                RemoveIngredient();
            }
        }

        Item item = PlayerController.player.inventory.GetHotbarItem();
        if (item != null)
        {
            Ingredient toAdd = item.GetComponent<Ingredient>();
            if(toAdd != null)
            {
                if(AddIngredient(toAdd))
                {
                    AddIngredient(Inventory.InstantiateItem(item, this.transform).GetComponent<Ingredient>());
                    PlayerController.player.inventory.RemoveSelectedHotbarItem();
                }
            }
        }
    }

    public void RemoveIngredient()
    {
        this.ingredient = null;
        UIManager.toolInfoUi.RemoveIngredient();
    }

    private void UpdateUi()
    {
        UIManager.toolInfoUi.UpdateUi();
    }
}
