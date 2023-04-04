using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistRandomiser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform[] clouds = GetComponentsInChildren<Transform>();

        foreach (Transform cloud in clouds)
        {
            cloud.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
