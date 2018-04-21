using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableSlider : Slider, IEndDragHandler
{
  public Action<float> OnRelease;
  
  public void OnEndDrag(PointerEventData eventData)
  {
    if (OnRelease != null) OnRelease(value);
  }
}