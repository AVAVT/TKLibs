using UnityEngine;

[System.Serializable]
public class CurvableValue
{
  [SerializeField] public AnimationCurve curve;
  [SerializeField] public float minValue;
  [SerializeField] public float maxValue;
  [SerializeField] public float start;
  [SerializeField] public float end;
  [SerializeField] public bool zeroBelowStart;

  public float ValueAt(float x)
  {
    float rate = end - start != 0 ? (x - start) / (end - start) : 1;
    return (zeroBelowStart && x < start) ? 0 : TKUtils.ValueFromCurve(curve, rate, minValue, maxValue);
  }

  public int IntValueAt(float x)
  {
    float rate = end - start != 0 ? (x - start) / (end - start) : 1;
    return (zeroBelowStart && x < start) ? 0 : TKUtils.IntValueFromCurve(curve, rate, minValue, maxValue);
  }
}