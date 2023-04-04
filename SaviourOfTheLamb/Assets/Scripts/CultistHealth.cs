using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistHealth : MonoBehaviour
{
    public bool isTank;
    public float health, invincibleTime, invincibleTimer;
    public GameObject deathSound, bloodSplat, corpse;

    // Start is called before the first frame update
    void Start()
    {
        if (isTank)
            GetComponent<TankCultistArmour>().maxHealth = health;;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleTimer > 0)
        invincibleTimer -= Time.deltaTime;
    }

    public void DamageCultist(float damage)
    {
        if (invincibleTimer <= 0)
        {
            health -= damage;
            invincibleTimer = invincibleTime;
            Instantiate(bloodSplat, transform.position, transform.rotation);

            if (isTank)
            {
                GetComponent<TankController>().hitTimer = GetComponent<TankController>().hitTime; // Reset timer when the cultist is hit (so the player doesn't trade blows but dominates the enemy).
                GetComponent<TankCultistArmour>().UpdateArmour(health);
            } 
        }

        if (health <= 0)
        {
            SnatcherCultist script = GetComponent<SnatcherCultist>();

            if (script != null && script.hasLamb)
            {
                script.lamb.GetComponent<NewLambAI>().enabled = true;
                //script.lamb.GetComponent<LambHealth>().enabled = true;
                script.lamb.GetComponent<Collider2D>().enabled = true;
            }

            StaticScript.cultistCount--;
            StaticScript.cultistsKilled++;
            //GetComponent<SpriteRenderer>().color = Color.black;
            //GetComponent<SpriteRenderer>().sortingLayerName = "Fence Behind Player";
            Instantiate(deathSound, transform.position, transform.rotation);

            /*if (GetComponent<CrossbowCultist>() != null)
            {
                Destroy(GetComponent<CrossbowCultist>().crossbow);
                Destroy(GetComponent<CrossbowCultist>());
            }

            if (GetComponent<TankController>() != null)
                Destroy(GetComponent<TankController>());

            if (GetComponent<SnatcherCultist>() != null)
                Destroy(GetComponent<SnatcherCultist>());

            Destroy(GetComponent<Collider2D>());*/

            for (int i = 0; i < 3; i++)
            {
                Vector3 newLocation = transform.position;
                newLocation.x += Random.Range(-0.5f, 0.5f);
                newLocation.y += Random.Range(-0.5f, 0.5f);
                Instantiate(bloodSplat, newLocation, transform.rotation);
            }

            Instantiate(corpse, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
