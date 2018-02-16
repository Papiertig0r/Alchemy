using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public static IngredientInfoUI ingredientInfoUi;
    public static ConsumableInfoUi consumableInfoUi;
    public static ToolInfoUI toolInfoUi;
    public static InventorySubmenuUI inventorySubmenuUi;
    public static ContainerUI containerUI;

    public static UIStates state = UIStates.NONE;

    public List<InventoryUI> activeUis = new List<InventoryUI>();
    public int activeUi;

    public IngredientInfoUI _ingredientInfoUi;
    public ConsumableInfoUi _consumableInfoUi;
    public ToolInfoUI _toolInfoUi;
    public InventorySubmenuUI _inventorySubmenuUi;
    public ContainerUI _containerUI;

    void Awake ()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != this)
        {
            Destroy(this);
        }

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

        if (inventorySubmenuUi == null)
        {
            inventorySubmenuUi = _inventorySubmenuUi;
        }

        if (containerUI == null)
        {
            containerUI = _containerUI;
        }
    }

    public void Update()
    {
        if(activeUis.Count <= 1)
        {
            return;
        }
        int change = Input.GetButtonDown("SwitchLeft") ? -1 : 0;
        change += Input.GetButtonDown("SwitchRight") ? 1 : 0;

        if(change != 0)
        {
            activeUi += change;
            activeUi = InventoryUI.WrapAround(activeUi, 0, activeUis.Count - 1);

            Select();
        }
    }

    public void Register(InventoryUI inventoryUi)
    {
        if(!activeUis.Contains(inventoryUi))
        {
            activeUis.Add(inventoryUi);
        }
        activeUi = activeUis.Count - 1;
        Select();
    }

    public void Unregister(InventoryUI inventoryUi)
    {
        activeUis.Remove(inventoryUi);
        activeUi = activeUis.Count - 1;
        Select();
    }

    public void Select()
    {
        if(activeUis.Count == 0)
        {
            return;
        }
        for(int i = 0; i < activeUis.Count; i++)
        {
            activeUis[i].Unselect();
        }

        activeUis[activeUi].Select(activeUis.Count > 1);
    }
}
