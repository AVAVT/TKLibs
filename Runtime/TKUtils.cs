using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TKUtils : MonoBehaviour
{

  public static GameObject Instantiate(GameObject prefab, GameObject parent)
  {
    var newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform rectTransform)
    {
      rectTransform.anchoredPosition3D = Vector3.zero;
    }
    return newObject;
  }

  public static GameObject Instantiate(GameObject prefab, Transform parent)
  {
    var newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform rectTransform)
    {
      rectTransform.anchoredPosition3D = Vector3.zero;
    }
    return newObject;
  }

  public static T Instantiate<T>(GameObject prefab)
  {
    var newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform rectTransform)
    {
      rectTransform.anchoredPosition3D = Vector3.zero;
    }
    return newObject.GetComponent<T>();
  }

  public static T Instantiate<T>(GameObject prefab, GameObject parent)
  {
    var newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform rectTransform)
    {
      rectTransform.anchoredPosition3D = Vector3.zero;
    }
    return newObject.GetComponent<T>();
  }

  public static T Instantiate<T>(GameObject prefab, Transform parent)
  {
    var newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform rectTransform)
    {
      rectTransform.anchoredPosition3D = Vector3.zero;
    }
    return newObject.GetComponent<T>();
  }

  // Better Lerp that support over/under shot

  public static float Lerp(float start, float end, float value)
  {
    return start + (end - start) * value;
  }

  public static Vector2 Lerp(Vector2 start, Vector2 end, float value)
  {
    return start + (end - start) * value;
  }

  public static Vector3 Lerp(Vector3 start, Vector3 end, float value)
  {
    return start + (end - start) * value;
  }

  public static float ValueFromCurve(AnimationCurve curve, float x, float min, float max)
  {
    return Mathf.Lerp(min, max, curve.Evaluate(x));
  }

  public static int IntValueFromCurve(AnimationCurve curve, float x, float min, float max)
  {
    return Mathf.RoundToInt(
      ValueFromCurve(curve, x, min, max)
    );
  }

  public static bool Dice(int faces, int successChance)
  {
    if (faces <= 0) throw new Exception("You cannot roll a dice with " + faces + " faces!");
    return Random.Range(0, faces) < successChance;
  }
}
