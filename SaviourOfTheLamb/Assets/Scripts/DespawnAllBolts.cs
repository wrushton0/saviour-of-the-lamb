using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAllBolts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] bolts = GameObject.FindGameObjectsWithTag("Bolt");
        foreach (GameObject bolt in bolts)
            GameObject.Destroy(bolt);
    }
}
