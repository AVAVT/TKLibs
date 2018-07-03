using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
    bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

    bool wasEnabled = GUI.enabled;
    GUI.enabled = enabled;
    if (!condHAtt.HideInInspector || enabled)
    {
      DrawGUI(position, property, label);
    }

    GUI.enabled = wasEnabled;
  }

  public virtual void DrawGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    EditorGUI.PropertyField(position, property, label, true);
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
  {
    ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
    bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

    if (!condHAtt.HideInInspector || enabled)
    {
      return EditorGUI.GetPropertyHeight(property, label);
    }
    else
    {
      return -EditorGUIUtility.standardVerticalSpacing;
    }
  }

  private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
  {
    bool enabled = true;
    string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
    string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
    SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

    if (sourcePropertyValue != null)
    {
      enabled = sourcePropertyValue.boolValue;
    }
    else if (!string.IsNullOrEmpty(condHAtt.ConditionalSourceField))
    {
      Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + property.name + " doesn't have property" + condHAtt.ConditionalSourceField);
    }

    return enabled;
  }
}