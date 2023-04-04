using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public float hitForce, damage, screenShakeForce;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Vector3 dirToPlayer = (Vector3) StaticScript.playerPosition - transform.position;
            col.GetComponent<Rigidbody2D>().AddForce(dirToPlayer * hitForce);
            col.GetComponent<PlayerController>().DamagePlayer(damage);
            StaticScript.mainCamera.GetComponent<CameraController>().ShakeScreen(dirToPlayer, screenShakeForce);
        }

        if (col.CompareTag("Lamb"))
        {
            // Knock back lamb
            Vector3 dirToLamb = (Vector3)StaticScript.lambPosition - transform.position;
            col.GetComponent<Rigidbody2D>().AddForce(dirToLamb * hitForce);
            //col.GetComponent<PlayerController>().DamagePlayer(damage);
        }

        if (col.CompareTag("Terrain") || col.CompareTag("Barricade"))
            Destroy(gameObject);
    }
}
