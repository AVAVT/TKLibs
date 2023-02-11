using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TKLibs
{
  public class DraggableSlider : Slider, IEndDragHandler, IPointerClickHandler
  {
    public Action<float> OnRelease;

    public void OnEndDrag(PointerEventData eventData)
    {
      OnRelease?.Invoke(value);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      OnRelease?.Invoke(value);
    }
  }
}