using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCreditCrawl : MonoBehaviour
{
    public float speed, maxY;
    public GameObject fadeToBlack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * speed, 0);

        if (transform.position.y > maxY)
            fadeToBlack.SetActive(true);
    }
}
