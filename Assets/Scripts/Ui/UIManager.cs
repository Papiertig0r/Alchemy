using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static IngredientInfoUI ingredientInfoUi;
    public static ConsumableInfoUi consumableInfoUi;
    public static ToolInfoUI toolInfoUi;

    public IngredientInfoUI _ingredientInfoUi;
    public ConsumableInfoUi _consumableInfoUi;
    public ToolInfoUI _toolInfoUi;

    void Start ()
    {
        if (ingredientInfoUi == null)
        {
            ingredientInfoUi = _ingredientInfoUi;
        }

        if (consumableInfoUi == null)
        {
            consumableInfoUi = _consumableInfoUi;
        }

        if (toolInfoUi == null)
        {
            toolInfoUi = _toolInfoUi;
        }
    }
}
