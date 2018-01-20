using UnityEngine;

[System.Serializable]
public class CurvableValue
{
  [SerializeField] public AnimationCurve curve;
  [SerializeField] public float min;
  [SerializeField] public float max;
  [SerializeField] public float start;
  [SerializeField] public float end;

  public float ValueAt(float x)
  {
    return TKUtils.ValueFromCurve(curve, (x - start) / (end - start), min, max);
  }

  public int IntValueAt(float x)
  {
    return TKUtils.IntValueFromCurve(curve, (x - start) / (end - start), min, max);
  }
}