public class SelectableSortingLayerAttribute : ConditionalHideAttribute
{
  public SelectableSortingLayerAttribute() : base() { }
  public SelectableSortingLayerAttribute(string conditionalSourceField = "", bool hideInInspector = false)
    : base(conditionalSourceField, hideInInspector) { }
}