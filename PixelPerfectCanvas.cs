using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PixelPerfectCanvas : MonoBehaviour
{
  private PixelPerfectCamera _ppc;
  private CanvasScaler _canvasScaler;

  private void Awake()
  {
    _ppc = Camera.main.GetComponent<PixelPerfectCamera>();
    _canvasScaler = GetComponent<CanvasScaler>();
  }

  private void Update()
  {
    if (_ppc.cameraPixelsPerUnit != _canvasScaler.scaleFactor)
    {
      _canvasScaler.scaleFactor = _ppc.cameraPixelsPerUnit;
    }
  }
}
