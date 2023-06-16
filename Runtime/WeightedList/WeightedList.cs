using System;
using System.Collections.Generic;
using System.Linq;

namespace TKLibs
{
  public record WeightedItem<T>(T Item, int Weight);
  
  public class WeightedList<T>
  {
    readonly Dictionary<T, WeightedItem<T>> _itemCache = new();
    readonly Random _random;

    int _currentMaxWeight = 0;
    
    /// <summary>
    /// Create a new WeightedList.
    /// </summary>
    /// <param name="random">Provide predefined random for procedural behavior. If null a default Random is used.</param>
    public WeightedList(Random random = null)
    {
      _random = random ?? new Random();
    }
    
    /// <summary>
    /// Get a random item from the list based on their weight.
    /// An item with weight value of 2 will have double the chance to be chosen compared with an item with weight 1.
    /// 
    /// If removal of result item is desired, use RemoveRandomItem
    /// </summary>
    /// <returns>Random item based on weight</returns>
    /// <exception cref="Exception"></exception>
    public T RandomItem()
    {
      if(_currentMaxWeight <= 0) throw new IndexOutOfRangeException(
        $"Unable to get random item in list because list is empty or all items are weightless. Total list weight is: {_currentMaxWeight}"
      );
      
      var ran = _random.Next(0, _currentMaxWeight);
      var weight = 0;
      foreach (var kvp in _itemCache)
      {
        weight += kvp.Value.Weight;
        if (ran < weight) return kvp.Key;
      }
      
      throw new(
        $"Unable to get random item in list. This is likely due to an error with the library. Random value is {ran}, total list weight is {_currentMaxWeight}"
      );
    }
    
    /// <summary>
    /// Remove a random item from the list based on their weight.
    /// An item with weight value of 2 will have double the chance to be chosen compared with an item with weight 1.
    ///
    /// If removal of result item is NOT desired, use RandomItem
    /// </summary>
    /// <returns>Removed item</returns>
    public T RemoveRandomItem()
    {
      var item = RandomItem();
      Remove(item);
      return item;
    }
    
    /// <summary>
    /// Add or Replace an item.
    /// If the same Item already exists in list, its Weight will be replaced with the provided item's Weight 
    /// </summary>
    /// <param name="weightedItem">Item to add to list</param>
    /// <returns>The provided weightedItem</returns>
    public WeightedItem<T> AddOrReplace(WeightedItem<T> weightedItem)
    {
      _itemCache[weightedItem.Item] = weightedItem;
      _currentMaxWeight = _itemCache.Values.Sum(i => i.Weight);
      
      return weightedItem;
    }
    
    /// <summary>
    /// Add or Replace an item with default Weight value of 1.
    /// If the same Item already exists in list, its Weight will be set to 1.
    /// </summary>
    /// <param name="item">Item to add to list</param>
    /// <returns>A WeightedItem created from provided Item with Weight 1</returns>
    public WeightedItem<T> AddOrReplace(T item) => AddOrReplace(new WeightedItem<T>(item, 1));
    
    /// <summary>
    /// Remove an item from the list
    /// </summary>
    /// <param name="item">Item key to remove</param>
    /// <returns>true if item successfully removed, false otherwise</returns>
    public bool Remove(T item)
    {
      var result = _itemCache.Remove(item);
      
      if (result) _currentMaxWeight = _itemCache.Values.Sum(i => i.Weight);

      return result;
    }
    
    /// <summary>
    /// Remove an item from the list
    /// </summary>
    /// <param name="weightedItem">Item key to remove. Weight is ignored in removal</param>
    /// <returns>true if item successfully removed, false otherwise</returns>
    public bool Remove(WeightedItem<T> weightedItem) => Remove(weightedItem.Item);
    
    /// <summary>
    /// Get the WeightedItem corresponding to provided Item key 
    /// </summary>
    /// <param name="item">Item key to look for</param>
    /// <returns>WeightedItem in list if exists, or null otherwise</returns>
    public WeightedItem<T> GetWeightedItem(T item) =>  _itemCache.TryGetValue(item, out var weightedItem) ? weightedItem : null;
  }
}