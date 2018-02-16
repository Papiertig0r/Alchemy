using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ContainerUI))]
public class ContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Add slots"))
        {
            ContainerUI containerUi = (ContainerUI)target;
            //containerUi.SetUp(1);
        }
    }
}
