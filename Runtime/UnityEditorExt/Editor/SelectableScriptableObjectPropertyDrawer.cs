using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(SelectableScriptableObjectPropertyAttribute))]
public class SelectableScriptableObjectPropertyDrawer : PropertyDrawer
{
  bool infoShown = false;
  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
  {
    return infoShown ? (EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2) : base.GetPropertyHeight(property, label);
  }

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    SelectableScriptableObjectPropertyAttribute selectableScriptableObjectPropertyAttribute = (SelectableScriptableObjectPropertyAttribute)attribute;
    var theType = selectableScriptableObjectPropertyAttribute.Type;
    var method = typeof(EditorHelpers).GetMethod(nameof(EditorHelpers.GetAllScriptableObjectInstances));
    var genericMethod = method.MakeGenericMethod(theType);
    var invoked = genericMethod.Invoke(null, null) as object[];
    var choices = invoked.Select(obj => new UnityEditor.SerializedObject(obj as UnityEngine.Object)?.FindProperty(selectableScriptableObjectPropertyAttribute.PropertyName)?.stringValue ?? "").ToArray();

    if (property.propertyType == SerializedPropertyType.String)
    {
      int index = Array.IndexOf(choices, property.stringValue);
      EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property);
      if (string.IsNullOrEmpty(property.stringValue))
      {
        infoShown = false;
      }
      else if (index < 0)
      {
        infoShown = true;
        EditorGUI.HelpBox(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight * 2), "No item found", MessageType.Error);
      }
      else
      {
        infoShown = true;
        EditorGUI.HelpBox(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight * 2), invoked[index].ToString(), MessageType.Info);
      }
    }
    else
    {
      EditorGUI.PropertyField(position, property, label, true);
    }
  }
}