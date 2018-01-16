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
            Consumable consumable = slot.item.GetComponent<Consumable>();
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
