using UnityEngine;
using System.Collections;

public class TKUtils : MonoBehaviour
{

  public static GameObject Instantiate(GameObject prefab, GameObject parent)
  {
    GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform) as GameObject;
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform)
    {
      (newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
    }
    return newObject;
  }

  public static GameObject Instantiate(GameObject prefab, Transform parent)
  {
    GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent) as GameObject;
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform)
    {
      (newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
    }
    return newObject;
  }

  public static T Instantiate<T>(GameObject prefab)
  {
    GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform)
    {
      (newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
    }
    return newObject.GetComponent<T>();
  }

  public static T Instantiate<T>(GameObject prefab, GameObject parent)
  {
    GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform) as GameObject;
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform)
    {
      (newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
    }
    return newObject.GetComponent<T>();
  }

  public static T Instantiate<T>(GameObject prefab, Transform parent)
  {
    GameObject newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent) as GameObject;
    newObject.transform.localScale = Vector3.one;
    if (newObject.transform is RectTransform)
    {
      (newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
    }
    return newObject.GetComponent<T>();
  }


  // Better Lerps that support over/under shot

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
    if (faces <= 0) throw new System.Exception("You cannot roll a dice with " + faces + " faces!");
    return Random.Range(0, faces) < successChance;
  }
}
