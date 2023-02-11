using UnityEngine;
using UnityEngine.UI;

namespace TKLibs
{
  public class FPSCounter : MonoBehaviour
  {
    const float UPDATE_INTERVAL = 0.5f;

    float _accum; // FPS accumulated over the interval
    float _frames; // Frames drawn over the interval
    float _timeLeft; // Left time for current interval
    Text _textDisplay;

    void Start()
    {
      _timeLeft = UPDATE_INTERVAL;
      _textDisplay = GetComponent<Text>();
    }

    void Update()
    {
      _timeLeft -= Time.deltaTime;
      _accum += Time.timeScale / Time.deltaTime;
      ++_frames;

      // Interval ended - update GUI text and start new interval
      if (_timeLeft <= 0.0)
      {
        // display two fractional digits (f2 format)
        _textDisplay.text = (_accum / _frames).ToString("0.##");
        _timeLeft = UPDATE_INTERVAL;
        _accum = 0;
        _frames = 0;
      }
    }
  }
}