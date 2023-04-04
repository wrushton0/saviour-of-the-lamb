using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnatcherStealsLamb : MonoBehaviour
{
    public float timer;
    public GameObject fadeToBlack;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.gameObject.GetComponent<CameraController>().player = transform;
        StaticScript.inTextScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            GameObject fade = Instantiate(fadeToBlack, transform.position, transform.rotation);
            fade.GetComponent<SceneTransition>().sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            fade.GetComponent<SceneTransition>().blackCover = StaticScript.blackCover;
            this.enabled = false;
        }
    }
}
