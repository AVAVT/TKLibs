using System;
using UnityEngine;

public class SelectableStringAttribute : ConditionalHideAttribute
{
  public string[] Choices
  {
    get;
    private set;
  }
  public SelectableStringAttribute(Type type)
  {
    Init(type);
  }

  public SelectableStringAttribute(Type type, string conditionalSourceField = "", bool hideInInspector = false)
    : base(conditionalSourceField, hideInInspector)
  {
    Init(type);
  }

  void Init(Type type)
  {
    var method = type.GetMethod("Values");
    if (method != null)
    {
      Choices = method.Invoke(null, null) as string[];
    }
    else
    {
      Debug.LogError("NO SUCH METHOD Values FOR " + type);
    }
  }
}