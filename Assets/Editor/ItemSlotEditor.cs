using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class ItemSlotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        Inventory inv = (Inventory)target;
        foreach (ItemSlot slot in inv.itemSlots)
        {
            if(slot.item == null)
            {
                continue;
            }
            IConsumable consumable = slot.item.GetComponent<IConsumable>();
            if(consumable != null)
            {
                if (GUILayout.Button("Consume " + slot.item.name))
                {
                    consumable.Consume(PlayerController.player);
                    inv.RemoveItemFromSlot(slot);
                }
            }
        }
    }
}
