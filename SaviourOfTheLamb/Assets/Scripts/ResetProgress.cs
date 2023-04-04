using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("currentScene", 0);
        PlayerPrefs.SetInt("completedGame", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
