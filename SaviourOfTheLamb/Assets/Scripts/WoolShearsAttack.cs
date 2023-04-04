using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolShearsAttack : MonoBehaviour
{

    public float baseDamage;
    public float damage;
    public float hitForce;
    public GameObject stabSound, swishSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Instantiate(swishSound, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        {
            if (col.CompareTag("Cultist"))
            {
                col.GetComponent<Rigidbody2D>().AddForce(transform.right * hitForce);
                col.GetComponent<CultistHealth>().DamageCultist(damage);
                damage = damage * 2 / 3;
                Instantiate(stabSound, transform.position, transform.rotation);
            }

            if (col.CompareTag("Lamb"))
            {
                //col.GetComponent<Rigidbody2D>().AddForce(-transform.right * hitForce);
                //col.GetComponent<LambHealth>().DamageLamb(damage);
                //damage = damage * 2 / 3;
            }
            if(col.CompareTag("Barricade"))
            {
                col.GetComponent<BarricadeController>().DamageBarricade(damage);
                damage = damage * 2 / 3;
            }
            if (col.CompareTag("Bolt"))
                Destroy(col.gameObject);
        }
    }
}
