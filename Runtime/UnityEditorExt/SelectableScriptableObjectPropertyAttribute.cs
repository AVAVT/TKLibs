using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class SelectableScriptableObjectPropertyAttribute : PropertyAttribute
{
  public Type Type;
  public string PropertyName;

  public SelectableScriptableObjectPropertyAttribute(Type type, string propertyName)
  {
    Type = type;
    PropertyName = propertyName;
  }
}