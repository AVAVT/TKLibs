using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WeightedList
{
  public static T RandomWeightedItem<T>(this List<WeightedItem<T>> list, System.Random random = null)
  {
    if (random == null) random = new System.Random();
    float ratio = list.Sum(i => i.weight) / 100;

    if (ratio == 0) throw new System.Exception("Unable to find random item in list. Total list weight is: " + list.Sum(e => e.weight));

    int ran = random.Next(0, 100);
    float weight = 0;

    foreach (WeightedItem<T> item in list)
    {
      weight += item.weight / ratio;
      if (ran < weight)
      {
        return item.item;
      }
    }

    throw new System.Exception("Unable to find random item in list. Total list weight is: " + list.Sum(e => e.weight));
  }
}