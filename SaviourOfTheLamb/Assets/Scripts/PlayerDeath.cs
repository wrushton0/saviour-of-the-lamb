using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject fadeToBlack;
    public float zoomSpeed;
    bool done;

    public float timeBeforeReset;

    void Start()
    {
        StaticScript.mainCamera.GetComponent<CameraController>().player = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        timeBeforeReset -= Time.deltaTime;
        if(timeBeforeReset < 0)
        {
            GameObject fade = Instantiate(fadeToBlack, transform.position, transform.rotation);
            fade.SetActive(true);
            fade.GetComponent<SceneTransition>().sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            fade.GetComponent<SceneTransition>().blackCover = StaticScript.blackCover;
            this.enabled = false;
        }
        else if(!done)
        {
            done = true;
            if (StaticScript.usingCrossbow)
            {
                StartCoroutine(ZoomCamera(8f, 4f, 2f, 80f));
            } else
            {
                StartCoroutine(ZoomCamera(6.67f, 4f, 2f, 80f));
            }
        }
    }

    IEnumerator ZoomCamera(float from, float to, float time, float steps)
    {
        float f = 0;

        while (f <= 1)
        {
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(from, to, f);

            f += 1f / steps;

            yield return new WaitForSeconds(time / steps);
        }
    }
}
