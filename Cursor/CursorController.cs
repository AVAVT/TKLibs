using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
  public static CursorController Instance { get; private set; }
  public Sprite defaultCursor;
  public Sprite highlightCursor;
  public Sprite disabledCursor;
  public CanvasScaler canvasScaler;

  Image cursorImage;
  private void Awake()
  {
    Instance = this;
    cursorImage = GetComponent<Image>();
    canvasScaler = GetComponentInParent<CanvasScaler>();
  }
  void Start()
  {
    UseDefaultCursor();
  }

  private void Update()
  {
    (transform as RectTransform).anchoredPosition = Input.mousePosition / canvasScaler.scaleFactor;
  }

  void UseCursor(Sprite cursor)
  {
    try
    {
      Cursor.visible = false;
      cursorImage.sprite = cursor;
    }
    catch (System.Exception e)
    {
      Debug.LogWarning(e);
    }
  }

  public void UseDefaultCursor()
  {
    UseCursor(defaultCursor);
  }

  public void UseHighLightCursor()
  {
    UseCursor(highlightCursor);
  }

  public void UseDisabledCursor()
  {
    UseCursor(disabledCursor);
  }
}
