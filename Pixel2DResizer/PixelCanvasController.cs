using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

#if PIXEL_PERFECT
public class PixelCanvasController : MonoBehaviour
{
  PixelPerfectCamera ppc;
  int scale;
  private void Start()
  {
    ppc = (FindObjectOfType(typeof(PixelPerfectCamera)) as PixelPerfectCamera);
    scale = ppc.pixelRatio;

    OnScaleChanged(scale);
  }

  private void Update()
  {
    if (ppc.pixelRatio != scale)
    {
      scale = ppc.pixelRatio;
      OnScaleChanged(scale);
    }
  }

  void OnScaleChanged(int scale)
  {
    GetComponent<CanvasScaler>().scaleFactor = scale;
  }
}
#endif