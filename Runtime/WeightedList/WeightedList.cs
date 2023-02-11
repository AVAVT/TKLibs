using System;
using System.Collections.Generic;
using System.Linq;

namespace TKLibs
{
  public static class WeightedList
  {
    public static T RandomWeightedItem<T>(this List<WeightedItem<T>> list, Random random = null)
    {
      random ??= new Random();
      
      var ratio = list.Sum(i => i.Weight) / 100;

      if (ratio == 0)
        throw new Exception(
          "Unable to find random item in list. Total list weight is: " + list.Sum(e => e.Weight)
        );

      var ran = random.Next(0, 100);
      float weight = 0;

      foreach (WeightedItem<T> item in list)
      {
        weight += item.Weight / ratio;
        if (ran < weight)
        {
          return item.Item;
        }
      }

      throw new Exception(
        "Unable to find random item in list. Total list weight is: " + list.Sum(e => e.Weight)
      );
    }
  }
}