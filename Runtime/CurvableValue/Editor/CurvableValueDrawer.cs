using UnityEngine;
using UnityEditor;

namespace TKLibs
{
  [CustomPropertyDrawer(typeof(CurvableValue))]
  public class CurvableValueDrawer : PropertyDrawer
  {
    const int FIELD_WIDTH = 50;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      var curve = property.FindPropertyRelative("curve").animationCurveValue;
      var min = property.FindPropertyRelative("min").floatValue;
      var max = property.FindPropertyRelative("max").floatValue;

      EditorGUI.CurveField(new Rect(position.x, position.y, FIELD_WIDTH, position.height), curve);

      EditorGUI.FloatField(
        new Rect(position),
        min
      );
      EditorGUI.FloatField(
        new Rect(position),
        max
      );
    }
  }
}