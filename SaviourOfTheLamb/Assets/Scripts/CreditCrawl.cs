using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCrawl : MonoBehaviour
{
    public int height;
    public float speed, yPosToGoToMenu;
    public GameObject fadeToBlack;

    private void Start()
    {
        height = Screen.currentResolution.height;
        print(Screen.currentResolution.height);
    }

    // Update is called once per frame
    void Update()
    {
        height = Screen.currentResolution.height;
        transform.position += new Vector3(0, Time.deltaTime * speed * ((float)height / 1080f), 0);

        if (transform.position.y > height)
            fadeToBlack.SetActive(true);
    }
}
