public class WeightedItem<T>
{
  public T item;
  public float percentage;

  public WeightedItem(T item, float percentage)
  {
    this.item = item;
    this.percentage = percentage;
  }
}