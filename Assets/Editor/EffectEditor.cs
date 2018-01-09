using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Effect))]
public class EffectEditor : Editor
{
    AnimationCurve curveX;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Effect effect = (Effect)target;

        List<Keyframe> keyframes = new List<Keyframe>();
        keyframes.Add(new Keyframe(effect.minConc, 0f));
        keyframes.Add(new Keyframe(effect.bestConc, 1f));
        keyframes.Add(new Keyframe(effect.maxConc, 0f));

        curveX = new AnimationCurve(keyframes.ToArray());

        EditorGUILayout.CurveField(curveX, Color.green, new Rect(0f, 0f, 100f, 1f));


    }
}
