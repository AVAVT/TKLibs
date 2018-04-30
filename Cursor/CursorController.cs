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

  Image cursorImage;
  private void Awake()
  {
    Instance = this;
    cursorImage = GetComponent<Image>();
  }
  void Start()
  {
    UseDefaultCursor();
  }

  private void Update()
  {
    (transform as RectTransform).anchoredPosition = (
      Input.mousePosition / GamePlayController.Instance.Services.gameWorldResizerService.Scale
    ).xy();
  }

  public void UseDefaultCursor()
  {
    Cursor.visible = false;
    cursorImage.sprite = defaultCursor;
  }

  public void UseHighLightCursor()
  {
    Cursor.visible = false;
    cursorImage.sprite = highlightCursor;
  }

  public void UseDisabledCursor()
  {
    Cursor.visible = false;
    cursorImage.sprite = disabledCursor;
  }
}
