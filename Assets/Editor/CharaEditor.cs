using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharaController), true)]
public class CharaEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharaController chara = (CharaController)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Editor functionalites", EditorStyles.boldLabel);

        if (GUILayout.Button("Take damage"))
        {
            //RepeatTimedBuff rtb = new RepeatTimedBuff(-2f, 10f, 1f);
            //rtb.Apply(chara.stats, chara.stats.health);
            TimedBuff tb = new TimedBuff(-2f, 10f);
            tb.Apply(chara.stats, chara.stats.healthRegen);
        }
    }
}
