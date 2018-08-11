using UnityEngine;

public static class ColorExtensionMethods
{
  public static Color WithR(this Color c, float r)
  {
    return new Color(r, c.g, c.b, c.a);
  }

  public static Color WithG(this Color c, float g)
  {
    return new Color(c.r, g, c.b, c.a);
  }

  public static Color WithB(this Color c, float b)
  {
    return new Color(c.r, c.g, b, c.a);
  }

  public static Color WithA(this Color c, float a)
  {
    return new Color(c.r, c.g, c.b, a);
  }
}