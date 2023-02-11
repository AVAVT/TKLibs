using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
  public string ConditionalSourceField;
  public bool HideInInspector;
  public bool Inverse;
  
  public ConditionalHideAttribute(string conditionalSourceField)
  {
    ConditionalSourceField = conditionalSourceField;
    HideInInspector = false;
    Inverse = false;
  }

  public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse = false)
  {
    ConditionalSourceField = conditionalSourceField;
    HideInInspector = hideInInspector;
    Inverse = inverse;
  }

  public ConditionalHideAttribute(bool hideInInspector = false)
  {
    ConditionalSourceField = "";
    HideInInspector = hideInInspector;
    Inverse = false;
  }
}