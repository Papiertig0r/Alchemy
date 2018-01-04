using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientType ingredientType;

    public List<PhaseTransition> phaseTransition = new List<PhaseTransition>();
    // e.g for herb
    // SOLID => PASTE w/ mortar & pestle
    // PASTE => LIQUID w/ distill
    // LIQUID => PASTE w/ calcinator
    // PASTE => POWDER w/ calcinator
}
