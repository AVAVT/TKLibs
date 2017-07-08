using UnityEngine;
using System.Collections;

public class TKCameraResizer : MonoBehaviour {
	public float deviceHalfWidth = 540.0f;

	void Awake(){
		Camera.main.orthographicSize = deviceHalfWidth / Camera.main.aspect;
	}
}