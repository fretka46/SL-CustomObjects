﻿namespace DONT_TOUCH.Scripts.Editors
{
    using System;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PrimitiveComponent))]
    public class PrimitiveEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Label($"Real in-game rotation: {GetRealRotation().eulerAngles.ToString()}");
        }

        private Quaternion GetRealRotation()
        {
            PrimitiveComponent primitiveComponent = (PrimitiveComponent)target;
            
            const sbyte range = sbyte.MaxValue;
            Quaternion quaternion = Quaternion.Euler(primitiveComponent.transform.localEulerAngles);
            Tuple<sbyte, sbyte, sbyte, sbyte> lpq = new(
                (sbyte)(quaternion.x * range),
                (sbyte)(quaternion.y * range),
                (sbyte)(quaternion.z * range),
                (sbyte)(quaternion.w * range));

            Quaternion recreatedQuaternion = new(lpq.Item1 / (float)range, lpq.Item2 / (float)range, lpq.Item3 / (float)range, lpq.Item4 / (float)range);
            return recreatedQuaternion;
        }
    }
}