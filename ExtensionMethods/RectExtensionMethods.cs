using UnityEngine;

public static class RectExtensionMethods
{
  public static Vector2 RandomPoint(this Rect rect)
  {
    return new Vector2(
      rect.x + Random.Range(0, rect.width),
      rect.y + Random.Range(0, rect.height)
    );
  }
}