using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
  float updateInterval = 0.5f;

  private float accum = 0f; // FPS accumulated over the interval
  private float frames = 0; // Frames drawn over the interval
  private float timeleft; // Left time for current interval
  private Text textDisplay;

  private void Start()
  {
    timeleft = updateInterval;
    textDisplay = GetComponent<Text>();
  }

  private void Update()
  {
    timeleft -= Time.deltaTime;
    accum += Time.timeScale / Time.deltaTime;
    ++frames;

    // Interval ended - update GUI text and start new interval
    if (timeleft <= 0.0)
    {
      // display two fractional digits (f2 format)
      textDisplay.text = (accum / frames).ToString("0.##");
      timeleft = updateInterval;
      accum = 0;
      frames = 0;
    }
  }
}
