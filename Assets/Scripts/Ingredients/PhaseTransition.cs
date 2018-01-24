using UnityEngine;
// e.g for herb
// SOLID => PASTE w/ mortar & pestle
// PASTE => LIQUID w/ distill
// LIQUID => PASTE w/ calcinator
// PASTE => POWDER w/ calcinator
[System.Serializable]
public class PhaseTransition
{
    public ToolType toolType;
    public Ingredient endProduct;
}
