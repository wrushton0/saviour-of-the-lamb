using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour
{
    
    public float startDelay, endDelay, letterTime, partTime;
    public Text textDisplay;
    public Transform centreCameraHere;
    public GameObject canvas;
    public List<string> parts;

    int partIndex, letterIndex; // 0 default value
    float timer; // 0 default value
    bool started, finished;
    GameObject player;

    public GameObject activeSpawners;

    // Start is called before the first frame update
    void Start()
    {
        timer = startDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            timer -= Time.deltaTime;

            /*if (Input.GetKeyDown(KeyCode.Return)) // To skip.
            {
                if (letterIndex < parts[partIndex].Length - 1) // If we still have at least one letter to print.
                {
                    letterIndex = parts[partIndex].Length - 1;
                    textDisplay.text = parts[partIndex].Substring(0, letterIndex);
                }
                else // Otherwise we have printed all the letters so skip to the next part.
                {
                    partIndex++;
                    letterIndex = 0;
                    textDisplay.text = parts[partIndex].Substring(0, letterIndex);
                    timer = letterTime;
                }
            }*/

            if (timer <= 0)
            {
                if (letterIndex < parts[partIndex].Length)
                {
                    letterIndex++;
                    timer = letterTime;
                    textDisplay.text = parts[partIndex].Substring(0, letterIndex);
                }
                else if (letterIndex == parts[partIndex].Length && partIndex < parts.Count - 1)
                {
                    partIndex++;
                    letterIndex = 0;
                    timer = partTime;
                }
                else if (!finished)
                {
                    timer = endDelay;
                    finished = true;
                }
                else
                {
                    textDisplay.text = "";
                    StaticScript.inTextScene = false;
                    Camera.main.GetComponent<CameraController>().player = player.transform;
                    textDisplay.gameObject.SetActive(false);
                    player.GetComponent<PlayerController>().enabled = true;

                    if(activeSpawners != null)
                    {
                        activeSpawners.SetActive(true);
                    }
                    // Don't need the below line as it generates 2 errors and something else must re-activate that object? Not sure what does it.
                    //player.GetComponentInChildren<WeaponParentController>().gameObject.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StaticScript.inTextScene = true;
            Camera.main.GetComponent<CameraController>().player = centreCameraHere;
            player = col.gameObject;
            player.GetComponent<PlayerController>().enabled = false;
            textDisplay.gameObject.SetActive(true);
            // Produces a error that means the game cannot continue.
            //player.GetComponentInChildren<WeaponParentController>().gameObject.SetActive(false);
            started = true;
            canvas.SetActive(true);
        }
    }
}
