using System.Collections.Generic;

public static class HashSetExtensionMethods
{
  public static T Any<T>(this HashSet<T> s) where T : class
  {
    foreach (var item in s) return item;
    return null;
  }
}