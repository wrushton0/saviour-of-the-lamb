using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject cultist in GameObject.FindGameObjectsWithTag("Cultist"))
            Destroy(cultist);
    }
}
