// https://gist.github.com/SeanMcTex/c28f6e56b803cdda8ed7acb1b0db6f82
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaCanvas : MonoBehaviour
{
  private Rect lastSafeArea;
  public RectTransform contentRectTransform;

  private RectTransform rectTransform;
  private void Start()
  {
    rectTransform = transform as RectTransform;
  }
  private void Update()
  {
    if (lastSafeArea != Screen.safeArea)
    {
      ApplySafeArea();
    }
  }

  private void ApplySafeArea()
  {
    Rect safeAreaRect = Screen.safeArea;

    float scaleRatio = rectTransform.rect.width / Screen.width;

    var left = safeAreaRect.xMin * scaleRatio;
    var right = -(Screen.width - safeAreaRect.xMax) * scaleRatio;
    var top = -safeAreaRect.yMin * scaleRatio;
    var bottom = (Screen.height - safeAreaRect.yMax) * scaleRatio;

    contentRectTransform.offsetMin = new Vector2(left, bottom);
    contentRectTransform.offsetMax = new Vector2(right, top);

    lastSafeArea = Screen.safeArea;
  }
}
