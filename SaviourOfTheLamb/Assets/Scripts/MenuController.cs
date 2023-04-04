using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button continueButton;
    public GameObject fadeToBlack, farmerAndLamb, cultists;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("currentScene", 0) == 0)
            continueButton.interactable = false;

        if (PlayerPrefs.GetInt("completedGame", 0) == 1)
            farmerAndLamb.SetActive(true);
        else
            cultists.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        fadeToBlack.GetComponent<SceneTransition>().nextScene = "Level1";
        fadeToBlack.SetActive(true);
    }

    public void Continue()
    {
        fadeToBlack.SetActive(true);
        fadeToBlack.GetComponent<SceneTransition>().sceneBuildIndex = PlayerPrefs.GetInt("currentScene", 1);
        fadeToBlack.GetComponent<SceneTransition>().useBuildIndex = true;
    }
}
