using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(SelectableSortingLayerAttribute))]
public class SelectableSortingLayerPropertyDrawer : PropertyDrawer
{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    SelectableSortingLayerAttribute selectableSortingLayerAttribute = (SelectableSortingLayerAttribute)attribute;

    if (property.propertyType == SerializedPropertyType.String)
    {
      int index = Array.IndexOf(SelectableSortingLayerAttribute.Choices, property.stringValue);

      if (index < 0)
      {
        Debug.LogWarning("WARNING: value" + property.stringValue + " of " + property.displayName + " is no longer available. Automatically using first value " + SelectableSortingLayerAttribute.Choices[0]);
        index = 0;
      }
      index = EditorGUI.Popup(position, property.displayName, index, SelectableSortingLayerAttribute.Choices);

      property.stringValue = SelectableSortingLayerAttribute.Choices[index];
    }
    else if (property.propertyType == SerializedPropertyType.Integer)
    {
      property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, SelectableSortingLayerAttribute.Choices);
    }
    else
    {
      base.OnGUI(position, property, label);
    }
  }
}