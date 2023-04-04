using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndOnKills : MonoBehaviour
{
    public int cultistsToKill;
    public GameObject spawner;
    public List<GameObject> removeTheseOnLevelWin, activateTheseOnLevelWin;

    // Start is called before the first frame update
    void Start()
    {
        StaticScript.cultistsKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //print(StaticScript.cultistsKilled);

        if (StaticScript.cultistsKilled >= cultistsToKill)
            spawner.SetActive(false);

        if (StaticScript.cultistsKilled >= cultistsToKill && StaticScript.cultistCount <= 0)
        {
            foreach (GameObject removeThisOnLevelWin in removeTheseOnLevelWin)
                Destroy(removeThisOnLevelWin);

            foreach (GameObject activateThisOnLevelWin in activateTheseOnLevelWin)
                activateThisOnLevelWin.SetActive(true);
        }
    }
}
