using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(SelectableStringAttribute))]
public class SelectableStringPropertyDrawer : ConditionalHidePropertyDrawer
{
  public override void DrawGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    SelectableStringAttribute selectableStringAttribute = (SelectableStringAttribute)attribute;

    if (property.propertyType == SerializedPropertyType.String)
    {
      int index = Array.IndexOf(selectableStringAttribute.Choices, property.stringValue);

      if (index < 0)
      {
        Debug.LogWarning("WARNING: value " + property.stringValue + " of " + property.displayName + " is no longer available. Automatically using first value " + selectableStringAttribute.Choices[0]);
        index = 0;
      }
      index = EditorGUI.Popup(position, property.displayName, index, selectableStringAttribute.Choices);

      property.stringValue = selectableStringAttribute.Choices[index];
    }
    else if (property.propertyType == SerializedPropertyType.Integer)
    {
      property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, selectableStringAttribute.Choices);
    }
    else
    {
      EditorGUI.PropertyField(position, property, label, true);
    }
  }
}