using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TKLibs
{
  public class CursorInteractor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    public Selectable targetSelectable;

    private void Start()
    {
      if (targetSelectable == null) targetSelectable = GetComponent<Selectable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (CursorController.Instance == null) return;
      
      if (targetSelectable.interactable) CursorController.Instance.UseHighLightCursor();
      else CursorController.Instance.UseDisabledCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if (CursorController.Instance == null) return;
      CursorController.Instance.UseDefaultCursor();
    }
  }
}