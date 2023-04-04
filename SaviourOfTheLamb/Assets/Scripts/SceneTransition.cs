using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public bool useBuildIndex;
    public int sceneBuildIndex;
    float initialTime;
    public enum transition { TO_BLACK, FROM_BLACK };
    public transition transitionDirection;
    public float timer;
    public Image blackCover;
    public string nextScene;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        if (StaticScript.blackCover != null)
            blackCover = StaticScript.blackCover;
        blackCover.gameObject.SetActive(true);
        initialTime = timer;
        //canvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if (transitionDirection == transition.TO_BLACK)
            blackCover.color = new Color(0, 0, 0, 1 - timer / initialTime);
        else
            blackCover.color = new Color(0, 0, 0, timer / initialTime);

        /*if (transitionDirection == transition.TO_BLACK)
            print(1 - timer / initialTime);
        else
            print(timer / initialTime);*/

        if (timer <= 0)
        {
            if (transitionDirection == transition.TO_BLACK)
                if (useBuildIndex)
                    SceneManager.LoadScene(sceneBuildIndex);
                else
                    SceneManager.LoadScene(nextScene);
            else
            {
                blackCover.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        if (Camera.main.GetComponent<AudioSource>() != null)
        {
            if (transitionDirection == transition.TO_BLACK)
                Camera.main.GetComponent<AudioSource>().volume = timer / initialTime;
            else
                Camera.main.GetComponent<AudioSource>().volume = 1 - timer / initialTime;
        }
    }
}
