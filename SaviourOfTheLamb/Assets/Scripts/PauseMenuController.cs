using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public bool paused = false;
    public float translucentBlackOpacity;
    public Image black;
    public GameObject pauseMenu, fadeToMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") && !paused)
        {
            paused = true;
            Time.timeScale = 0;
            black.color = new Color(0, 0, 0, translucentBlackOpacity);
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown("p") && paused)
        {
            paused = false;
            Time.timeScale = 1;
            black.color = new Color(0, 0, 0, 0);
            pauseMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        black.color = new Color(0, 0, 0, 0);
        pauseMenu.SetActive(false);
    }

    public void Menu()
    {
        fadeToMenu.SetActive(true);
        Time.timeScale = 1;
    }
}
