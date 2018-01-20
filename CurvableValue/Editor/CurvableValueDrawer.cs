// using UnityEngine;
// using UnityEditor;

// [CustomPropertyDrawer(typeof(CurvableValue))]
// public class CurvableValueDrawer : PropertyDrawer
// {
//   const int fieldwidth = 50;
//   public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//   {
//     var curve = property.FindPropertyRelative("curve") as AnimationCurve;
//     var min = property.FindPropertyRelative("min");
//     var max = property.FindPropertyRelative("max");

//     EditorGUI.CurveField(new Rect(position.x, position.y, fieldwidth, position.height), curve);

//     EditorGUIUtility.BeginHorizontal();
//     EditorGUI.FloatField(
//       new Rect(position), min
//     );
//     EditorGUI.FloatField(
//       new Rect(position), max
//     );
//     EditorGUIUtility.EndHorizontal();
//   }
// }