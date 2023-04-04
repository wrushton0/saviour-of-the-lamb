using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool invertScreenShake, hasCrossbow;
    public float lerpT, zPosition, slowLerpT, rangedCameraSize, meleeCameraSize, cameraSizeTransitionTime;
    public Transform player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        StaticScript.mainCamera = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lerpPosition;

        if (StaticScript.inTextScene)
            lerpPosition = Vector2.Lerp(transform.position, player.position, slowLerpT * Time.deltaTime); // May need to use Time.deltaTime here.
        else
            lerpPosition = Vector2.Lerp(transform.position, player.position, lerpT * Time.deltaTime);

        transform.position = new Vector3(lerpPosition.x, lerpPosition.y, zPosition);
        if (StaticScript.usingCrossbow && hasCrossbow)
        {
            //GetComponent<Camera>().orthographicSize = rangedCameraSize;
            //StartCoroutine(ZoomCamera(meleeCameraSize, rangedCameraSize, 1f, 4f));
        }
        else
        {
            //GetComponent<Camera>().orthographicSize = meleeCameraSize;
            //StartCoroutine(ZoomCamera(rangedCameraSize, meleeCameraSize, 1f, 4f));
        }
    }

    public void ShakeScreen(Vector2 direction, float force)
    {
        if (invertScreenShake)
            rb.AddForce(-direction * force);
        else
            rb.AddForce(direction * force);
    }

    //IEnumerator ZoomCamera(float from, float to, float time, float steps)
    //{
    //    float f = 0;

    //    while (f <= 1)
    //    {
    //        GetComponent<Camera>().orthographicSize = Mathf.Lerp(from, to, f);

    //        f += 1f / steps;

    //        yield return new WaitForSeconds(time / steps);
    //    }
    //}
}
