using UnityEngine;
using System.Collections;

public class TKUtils : MonoBehaviour{
	
	public static GameObject Instantiate(GameObject prefab, GameObject parent){
		GameObject newObject = Instantiate (prefab, Vector3.zero, Quaternion.identity, parent.transform) as GameObject;
		newObject.transform.localScale = Vector3.one;
		if (newObject.transform is RectTransform) {
			(newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
		}
		return newObject;
	}

	public static GameObject Instantiate(GameObject prefab, Transform parent){
		GameObject newObject = Instantiate (prefab, Vector3.zero, Quaternion.identity, parent) as GameObject;
		newObject.transform.localScale = Vector3.one;
		if (newObject.transform is RectTransform) {
			(newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
		}
		return newObject;
	}

	public static T Instantiate<T>(GameObject prefab, GameObject parent){
		GameObject newObject = Instantiate (prefab, Vector3.zero, Quaternion.identity, parent.transform) as GameObject;
		newObject.transform.localScale = Vector3.one;
		if (newObject.transform is RectTransform) {
			(newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
		}
		return newObject.GetComponent<T>();
	}

	public static T Instantiate<T>(GameObject prefab, Transform parent){
		GameObject newObject = Instantiate (prefab, Vector3.zero, Quaternion.identity, parent) as GameObject;
		newObject.transform.localScale = Vector3.one;
		if (newObject.transform is RectTransform) {
			(newObject.transform as RectTransform).anchoredPosition3D = Vector3.zero;
		}
		return newObject.GetComponent<T>();
	}
}
