using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using UnityEditorInternal;

public class SelectableSortingLayerAttribute : PropertyAttribute
{
  private static string[] choices;
  public static string[] Choices
  {
    get
    {
      if (choices == null) choices = GetSortingLayerNames();
      return choices;
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