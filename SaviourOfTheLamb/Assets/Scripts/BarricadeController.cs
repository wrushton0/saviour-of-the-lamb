using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour
{

    public float StartTimer, health, time;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 relativePos = StaticScript.lambPosition - StaticScript.playerPosition;
        //Quaternion rotation = Quaternion.LookRotation(relativePos, transform.right);
        //transform.rotation = rotation;

        float rotation_z = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 90);

        transform.position = (Vector3) StaticScript.playerPosition + Vector3.Scale(relativePos, new Vector3(.5f, .5f ,.5f));
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        StartTimer -= Time.deltaTime;
        //transform.rotation = Quaternion.LookRotation(lambPos);

        if(time < 0)
        {
            StaticScript.isBarricade = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col != null && StartTimer > 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageBarricade(float damage)
    {
        health -= damage;

        if (health < 0)
            StaticScript.isBarricade = false;
            Destroy(gameObject);
    }
}
