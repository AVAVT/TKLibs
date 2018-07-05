using UnityEngine;
using UnityEngine.UI;

public class PixelCanvasController : MonoBehaviour
{
  PixelGameWorldResizer gameWorldResizer;
  private void Start()
  {
    gameWorldResizer = (FindObjectOfType(typeof(PixelGameWorldResizer)) as PixelGameWorldResizer);
    OnScaleChanged(gameWorldResizer.Scale);
    gameWorldResizer.OnScaleChanged += OnScaleChanged;
  }

  private void OnDestroy()
  {
    if (gameWorldResizer != null) gameWorldResizer.OnScaleChanged -= OnScaleChanged;
  }

  void OnScaleChanged(int scale)
  {
    GetComponent<CanvasScaler>().scaleFactor = scale;
  }
}