public class WeightedItem<T>
{
  public T item;
  public float weight;

  public WeightedItem(T item, float weight)
  {
    this.item = item;
    this.weight = weight;
  }
}