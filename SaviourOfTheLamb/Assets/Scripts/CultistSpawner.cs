 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistSpawner : MonoBehaviour
{
    public int maxCultists, startingCultists;
    public float spawnMinDistFromPlayer, spawnChanceDrawTimer, cultistSpawnChanceDivisor; // Given a 0% chance to spawn a cultist if there are the maximum amount of cultists and 100% chance to spawn one if there are no cultists, the chance between these two values calculated by the number of active cultists is divided by cultistSpawnChanceDivisor.
    public List<GameObject> cultists;
    public List<Transform> spawns;
    Vector2 smallestDistancePos;

    // Start is called before the first frame update
    void Start()
    {
        spawnChanceDrawTimer = 1f;
        StaticScript.cultistCount = startingCultists;
    }

    // Update is called once per frame
    void Update()
    {
        spawnChanceDrawTimer -= Time.deltaTime;

        smallestDistancePos = new Vector2(999,999);

        if (spawnChanceDrawTimer <= 0)
        {
            if (Random.Range(0f, 1f) < (1f - (float)StaticScript.cultistCount / (float)maxCultists) / cultistSpawnChanceDivisor)
            {
                Vector2 spawnPos = Vector2.zero;

                foreach (Transform spawn in spawns)
                {
                    if (Vector2.Distance(StaticScript.playerPosition, spawn.position) > spawnMinDistFromPlayer)
                        spawnPos = spawn.position;
                    
                    if (Vector2.Distance(spawnPos, StaticScript.playerPosition) < Vector2.Distance(smallestDistancePos, StaticScript.playerPosition))
                    {
                        smallestDistancePos = spawnPos;
                    }
                }

                // If we don't select a spawn point, we should still spawn something otherwise the level will empty.
                if (spawnPos == Vector2.zero)
                    spawnPos = spawns[Random.Range(0, spawns.Count)].position;

                Instantiate(cultists[Random.Range(0, cultists.Count)], smallestDistancePos, transform.rotation);
                StaticScript.cultistCount++;
            }

            spawnChanceDrawTimer = 1f;
        }
        // spawn timer determined by total active cultists
        // spawn random cultist in out of sight spawner
    }
}
