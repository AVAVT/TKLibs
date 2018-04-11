using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WeightedList
{
  public static T RandomWeightedItem<T>(this List<WeightedItem<T>> list, System.Random random = null)
  {
    if (random == null) random = new System.Random();
    float ran = random.Next(0, 100) / 100f;
    float weight = 0;

    foreach (WeightedItem<T> item in list)
    {
      weight += item.percentage;
      if (ran < weight)
      {
        return item.item;
      }
    }

    throw new System.Exception("Unable to find random item in list. Total list weight is: " + list.Sum(e => e.percentage));
  }
}