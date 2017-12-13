using UnityEngine;


public static class BoundsExtensionMethods
{
  public static bool Intersect(this Bounds b1, Bounds b2){
    return b1.min.x <= b2.max.x && b1.max.x >= b2.min.x && b1.min.y <= b2.max.y && b1.max.y >= b2.min.y;
  }
  public static bool Intersect(this BoundsInt b1, BoundsInt b2){
    return b1.min.x <= b2.max.x && b1.max.x >= b2.min.x && b1.min.y <= b2.max.y && b1.max.y >= b2.min.y;
  }
}