using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolInfoUI : MonoBehaviour
{
    public Text toolName;
    public Slider progressSlider;
    public ItemSlotUI itemSlotUi;

    private Tool tool;

    public void Deactivate()
    {
        gameObject.SetActive(false);
        StateController.Transition(State.WORLD);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void Activate()
    {
        StateController.Transition(State.TOOL_UI);
        gameObject.SetActive(true);
    }

    public void UpdateUi()
    {
        float progress = 0f;
        if(tool != null && tool.ingredient != null)
        {
            progress = tool.ingredient.currentProgress;
        }
        progressSlider.value = progress;
    }

    public void UpdateIngredient()
    {
        itemSlotUi.SetSprite(tool.ingredient.GetComponent<SpriteRenderer>().sprite);
    }

    public void RemoveIngredient()
    {
        itemSlotUi.SetSprite(null);
    }

    public void SetTool(Tool tool)
    {
        this.tool = tool;
        toolName.text = tool.name;
    }
}
