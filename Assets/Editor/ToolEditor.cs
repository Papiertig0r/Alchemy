using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tool), true)]
public class ToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Process"))
        {
            Tool tool = (Tool)target;
            if(tool.ingredient != null)
            {
                tool.AdvanceIngredient();
            }
        }
    }
}
