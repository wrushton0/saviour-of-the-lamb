using System.Collections;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		StartCoroutine(ZoomCamera(20, 5, 5, 100));
	}

	IEnumerator ZoomCamera(float from, float to, float time, float steps)
	{
		float f = 0;

		while (f <= 1)
		{
			Camera.main.orthographicSize = Mathf.Lerp(from, to, f);

			f += 1f / steps;

			yield return new WaitForSeconds(time / steps);
		}
	}
}