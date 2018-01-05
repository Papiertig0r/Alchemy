using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public static float maxProgress = 100f;
    public static float minProgress = 0f;

    public IngredientType ingredientType;
    public ToolType currentToolType = ToolType.NONE;
    public float currentProgress = 0f;

    public List<PhaseTransition> phaseTransition = new List<PhaseTransition>();
    // e.g for herb
    // SOLID => PASTE w/ mortar & pestle
    // PASTE => LIQUID w/ distill
    // LIQUID => PASTE w/ calcinator
    // PASTE => POWDER w/ calcinator
}
