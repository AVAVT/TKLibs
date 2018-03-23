using UnityEngine;

[System.Serializable]
public class CurvableValue
{
  [SerializeField] public AnimationCurve curve;
  [SerializeField] public float min;
  [SerializeField] public float max;
  [SerializeField] public float start;
  [SerializeField] public float end;
  [SerializeField] public bool zeroBelowStart;

  public float ValueAt(float x)
  {
    return (zeroBelowStart && x < start) ? 0 : TKUtils.ValueFromCurve(curve, (x - start) / (end - start), min, max);
  }

  public int IntValueAt(float x)
  {
    return (zeroBelowStart && x < start) ? 0 : TKUtils.IntValueFromCurve(curve, (x - start) / (end - start), min, max);
  }
}