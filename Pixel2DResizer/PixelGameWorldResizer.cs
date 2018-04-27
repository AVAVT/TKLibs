using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelGameWorldResizer : MonoBehaviour
{

  [SerializeField]
  protected int scale = 1;

  public int Scale
  {
    get
    {
      return scale;
    }
    set
    {
      scale = value;
      transform.localScale = Vector3.one * value;
      if (OnScaleChanged != null) OnScaleChanged(value);
    }
  }

  public System.Action<int> OnScaleChanged;
  public Vector2Int minViewportSize;

  public void UpdateGameScaleForView(int width, int height)
  {
    Scale = Mathf.Max(Mathf.Min(
      width / minViewportSize.x,
      height / minViewportSize.y
    ), 1);

    transform.position = new Vector2(
      width / 2f % 1,
      height / 2f % 1
    );
  }
}
