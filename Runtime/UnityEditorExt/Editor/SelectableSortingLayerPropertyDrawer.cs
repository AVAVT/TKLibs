using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(SelectableSortingLayerAttribute))]
public class SelectableSortingLayerPropertyDrawer : ConditionalHidePropertyDrawer
{
  private static string[] choices;
  private static string[] Choices
  {
    get
    {
      if (choices == null)
      {
        choices = GetSortingLayerNames();
      }

      return choices;
    }
  }

  public override void DrawGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    SelectableSortingLayerAttribute selectableSortingLayerAttribute = (SelectableSortingLayerAttribute)attribute;

    if (property.propertyType == SerializedPropertyType.String)
    {
      int index = Array.IndexOf(Choices, property.stringValue);

      if (index < 0)
      {
        Debug.LogWarning("WARNING: value" + property.stringValue + " of " + property.displayName + " is no longer available. Automatically using first value " + Choices[0]);
        index = 0;
      }
      index = EditorGUI.Popup(position, property.displayName, index, Choices);

      property.stringValue = Choices[index];
    }
    else if (property.propertyType == SerializedPropertyType.Integer)
    {
      property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, Choices);
    }
    else
    {
      EditorGUI.PropertyField(position, property, label, true);
    }
  }

  private static string[] GetSortingLayerNames()
  {
    Type internalEditorUtilityType = typeof(InternalEditorUtility);
    PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
    var sortingLayers = (string[])sortingLayersProperty.GetValue(null, new object[0]);
    return sortingLayers;
  }
}