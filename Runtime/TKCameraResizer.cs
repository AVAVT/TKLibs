using UnityEngine;

namespace TKLibs
{
	public class TKCameraResizer : MonoBehaviour
	{
		public float deviceHalfWidth = 540.0f;

		void Awake()
		{
			Camera.main.orthographicSize = deviceHalfWidth / Camera.main.aspect;
		}
	}
}