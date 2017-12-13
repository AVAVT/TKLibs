using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class BoundsExtensionMethods
{
  public static bool Intersect(this Bounds b1, Bounds b2)
  {
    return b1.min.x <= b2.max.x && b1.max.x >= b2.min.x && b1.min.y <= b2.max.y && b1.max.y >= b2.min.y;
  }
  public static bool Intersect(this BoundsInt b1, BoundsInt b2)
  {
    return b1.min.x <= b2.max.x && b1.max.x >= b2.min.x && b1.min.y <= b2.max.y && b1.max.y >= b2.min.y;
  }

  public static List<Vector3Int> AllPoints(this BoundsInt bounds)
  {
    List<Vector3Int> result = new List<Vector3Int>();
    foreach (int x in Enumerable.Range(bounds.x, bounds.size.x + 1))
    {
      foreach (int y in Enumerable.Range(bounds.y, bounds.size.y + 1))
      {
        foreach (int z in Enumerable.Range(bounds.z, bounds.size.z + 1))
        {
          result.Add(new Vector3Int(x, y, z));
        }
      }
    }
    return result;
  }

  public static List<Vector2Int> All2DPoints(this BoundsInt bounds)
  {
    List<Vector2Int> result = new List<Vector2Int>();
    foreach (int x in Enumerable.Range(bounds.x, bounds.size.x + 1))
    {
      foreach (int y in Enumerable.Range(bounds.y, bounds.size.y + 1))
      {
        result.Add(new Vector2Int(x, y));
      }
    }
    return result;
  }
}