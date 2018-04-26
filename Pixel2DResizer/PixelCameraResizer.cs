using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCameraResizer : MonoBehaviour
{
  public PixelGameWorldResizer gameWorldResizer;

  private Vector2Int currenScreenSize = Vector2Int.zero;

  private void Update()
  {
    if (Screen.width != currenScreenSize.x || Screen.height != currenScreenSize.y)
    {
      currenScreenSize = new Vector2Int(Screen.width, Screen.height);
      GetComponent<Camera>().orthographicSize = Screen.height / 2f;
      gameWorldResizer.UpdateGameScaleForView(Screen.width, Screen.height);
    }
  }
}
