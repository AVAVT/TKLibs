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
  
  /*
   * Quick method to calculate the color luminance, useful for determine which text color to use over a background
   * source: https://stackoverflow.com/questions/596216/formula-to-determine-perceived-brightness-of-rgb-color
   */
  public static float ApproxLuminance(this Color c) => 0.2126f * c.r + 0.7152f * c.g + 0.0722f * c.b;
}