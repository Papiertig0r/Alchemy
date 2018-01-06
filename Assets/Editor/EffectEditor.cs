using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ingredient))]
public class EffectEditor : Editor
{
    AnimationCurve curveX;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Ingredient ingredient = (Ingredient)target;


        foreach (Effect effect in ingredient.effectList)
        {
            GUILayout.Label(effect.name);

            List<Keyframe> keyframes = new List<Keyframe>();
            keyframes.Add(new Keyframe(effect.minConc, 0f));
            keyframes.Add(new Keyframe(effect.bestConc, 1f));
            keyframes.Add(new Keyframe(effect.maxConc, 0f));

            curveX = new AnimationCurve(keyframes.ToArray());

            curveX = EditorGUILayout.CurveField(curveX, Color.green, new Rect(0f, 0f, 100f, 1f));

            effect.minConc = curveX.keys[0].time;
            effect.maxConc = curveX.keys[2].time;
            effect.bestConc = curveX.keys[1].time;

        }
    }
}
