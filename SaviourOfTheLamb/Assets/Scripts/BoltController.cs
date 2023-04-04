using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    public bool isPlayerBolt/*, stuckToCultist*/;
    public float hitForce, damage, screenShakeForce;
    public GameObject hitTerrain, hitCultist, arrowFly;
    //Vector2 stuckPosition;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject newArrowFly = Instantiate(arrowFly, transform.position, transform.rotation);
        newArrowFly.transform.parent = transform; // Whoosh/swish noise destroyed when bolt is, ensures short range bolt doesn't sound like it's gone a long way.
    }

    // Update is called once per frame
    void Update()
    {
        /*if (stuckToCultist)
        {
            transform.localPosition = stuckPosition;
        }*/
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isPlayerBolt && col.CompareTag("Cultist"))
        {
            col.GetComponent<Rigidbody2D>().AddForce(transform.right * hitForce);
            col.GetComponent<CultistHealth>().DamageCultist(damage);
            // Attach bolt to cultist.
            /*rb.velocity = Vector2.zero;
            transform.parent = col.transform;
            Destroy(GetComponent<TimedDestroy>());
            stuckToCultist = true;
            stuckPosition = transform.position;*/
            Instantiate(hitCultist, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (!isPlayerBolt && col.CompareTag("Player"))
        {
            if (col.GetComponent<PlayerController>().invincibleTimer <= 0)
            {
                col.GetComponent<Rigidbody2D>().AddForce(-transform.right * hitForce);
                col.GetComponent<PlayerController>().DamagePlayer(damage);
                StaticScript.mainCamera.GetComponent<CameraController>().ShakeScreen(transform.right, screenShakeForce);
                Destroy(gameObject);
            }
            
        }

        if (col.CompareTag("Lamb"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
            //col.GetComponent<Rigidbody2D>().AddForce(-transform.right * hitForce);
            //col.GetComponent<LambHealth>().DamageLamb(damage);
            //Destroy(gameObject);
        }

        if (col.CompareTag("Terrain") || col.CompareTag("Barricade"))
        {
            Instantiate(hitTerrain, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
