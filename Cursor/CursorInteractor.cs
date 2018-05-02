using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorInteractor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  public Selectable targetSelectable;
  private void Start()
  {
    if (targetSelectable == null) targetSelectable = GetComponent<Selectable>();
  }
  public void OnPointerEnter(PointerEventData eventData)
  {
    if (targetSelectable.interactable)
    {
      CursorController.Instance?.UseHighLightCursor();
    }
    else
    {
      CursorController.Instance?.UseDisabledCursor();
    }
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    CursorController.Instance?.UseDefaultCursor();
  }
}
