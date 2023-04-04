using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("completedGame", 1); // Mark game as completed.
        PlayerPrefs.SetInt("currentScene", 0); // Reset game.
    }
}
