using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{

    public GameObject magicObject, sound;

    Vector3 playerDirection;
    public float speed, spawnTimerLeft, spawnTimer, lifeTime;

    public void Start()
    {
        spawnTimerLeft = spawnTimer;
        playerDirection = (Vector3) StaticScript.playerPosition - transform.position;
        lifeTime = (Vector3.Distance(transform.position, StaticScript.playerPosition) / speed) + 1;
        Instantiate(sound, transform.position, transform.rotation);
    }

    public void Update()
    {
        spawnTimerLeft -= Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (spawnTimerLeft < 0)
        {
            spawnTimerLeft = spawnTimer;
            GameObject magic = Instantiate(magicObject);
            magic.transform.position = transform.position;
        }

        transform.position += playerDirection.normalized * speed * Time.deltaTime;

        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
