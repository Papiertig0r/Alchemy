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
    }
}
