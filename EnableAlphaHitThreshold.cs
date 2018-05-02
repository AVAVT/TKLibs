using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableAlphaHitThreshold : MonoBehaviour
{
  public float alphaThreshold = 0.1f;
  void Update()
  {
    GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
  }
}
