using System;
using UnityEngine;
using System.Collections.Generic;

public static class TransformExtensions
{
  public static void DestroyAllChildren(this Transform transform)
  {
    for (int i = transform.childCount - 1; i >= 0; i--)
    {
      GameObject.Destroy(transform.GetChild(i).gameObject);
    }
  }
}