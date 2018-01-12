using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharaController), true)]
public class CharaEditor : Editor
{
    private float damage;

    private void OnEnable()
    {
        if (EditorPrefs.HasKey("damage"))
        {
            damage = EditorPrefs.GetFloat("damage");
        }
    }

    private void OnDisable()
    {
        if(!Application.isPlaying)
        {
            EditorPrefs.SetFloat("damage", damage);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharaController chara = (CharaController)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Editor functionalites", EditorStyles.boldLabel);

        damage = EditorGUILayout.FloatField("Damage", damage);
        if (GUILayout.Button("Take damage"))
        {
            chara.TakeDamage(damage);
        }
    }
}
