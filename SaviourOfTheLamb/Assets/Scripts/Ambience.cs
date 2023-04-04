using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (StaticScript.ambienceExists)
            Destroy(gameObject);

        StaticScript.ambienceExists = true;
        DontDestroyOnLoad(gameObject);
    }

    /*public void Update()
    {
        print(PlayerPrefs.GetInt("currentScene", 0));
    }*/
}
