using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StaticScript.mainCamera.GetComponent<CameraController>().player = GameObject.Find("Lamb").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        StaticScript.mainCamera.GetComponent<Camera>().orthographicSize -= Time.deltaTime * speed;
    }
}
