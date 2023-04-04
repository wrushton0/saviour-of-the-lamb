using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourcerorCultistController : MonoBehaviour
{
    public float shootTimer;
    float shootTimerLeft;

    public float targetDistance;
    public float variance;
    //public float footstepTimer, footstepTime;
    //SoundPlayer soundPlayer;

    public GameObject magicAttack;
    public GameObject barricadeAttack;

    public float distanceToPlayer;
    Vector3 directionToPlayer;
    Vector3 directionToLamb;

    public float speed;
    float baseSpeed;
    RaycastHit2D ray;

    bool right = true;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        //soundPlayer = GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //footstepTimer -= Time.deltaTime;
        shootTimerLeft -= Time.deltaTime;

        directionToPlayer =  (Vector3) StaticScript.playerPosition - transform.position;
        directionToLamb = (Vector3)StaticScript.lambPosition - transform.position;

        distanceToPlayer = Vector3.Distance(StaticScript.playerPosition, transform.position);

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, directionToPlayer);
        RaycastHit2D hitLamb = Physics2D.Raycast(transform.position, directionToLamb);

        if ((targetDistance - variance) < distanceToPlayer && (targetDistance + variance) > distanceToPlayer && hitPlayer.collider.CompareTag("Player"))
        {
            if (shootTimerLeft < 0)
            {
                if (hitLamb.collider.CompareTag("Lamb"))
                {
                    float rand = Random.Range(0f, 1f);
                    // shoot magic
                    if (rand < 2f)
                    {
                        // Use Magic Attack
                        shootTimerLeft = shootTimer;
                        GameObject attack = Instantiate(magicAttack);
                        attack.transform.position = transform.position;
                    }
                    else if(!StaticScript.isBarricade)
                    {
                        //Barricade Player and Lamb
                        StaticScript.isBarricade = true;
                        shootTimerLeft = shootTimer;
                        Instantiate(barricadeAttack);
                    }
                }
                else
                {
                    // Use Magic Attack
                    shootTimerLeft = shootTimer;
                    GameObject attack = Instantiate(magicAttack);
                    attack.transform.position = transform.position;
                }
            }
        }
        else if ((targetDistance - variance) < distanceToPlayer && distanceToPlayer < (targetDistance + variance) && !hitPlayer.collider.CompareTag("Player"))
        {
            directionToPlayer = new Vector2(directionToPlayer.y, -directionToPlayer.x);

            if (right)
            {
                transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
                //CheckFootstep();
            }
            else
            {
                transform.position += -directionToPlayer.normalized * speed * Time.deltaTime;
                //CheckFootstep();
            }
        }
        else if (distanceToPlayer < (targetDistance - variance))
        {
            // Move away from player
            transform.position += -directionToPlayer.normalized * speed * Time.deltaTime;
            //CheckFootstep();
        }
        else if (distanceToPlayer > (targetDistance + variance))
        {
            // Move towards player
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;
            //CheckFootstep();
        }

        //shootTimerLeft -= Time.deltaTime;

        //distanceToPlayer = Vector3.Distance(transform.position, StaticScript.playerPosition);
        //directionTowardsPlayer = (Vector3) StaticScript.playerPosition - transform.position;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, directionTowardsPlayer);

        //// Attempt to Shoot
        //if ((targetDistance - variance) < distanceToPlayer && distanceToPlayer < (targetDistance + variance) && hit.collider.CompareTag("Player"))
        //{
        //    if (shootTimerLeft < 0)
        //    {

        //        float random = Random.Range(0.0f, 1f);
        //        RaycastHit2D hitLamb = Physics2D.Raycast(transform.position, ((Vector3)StaticScript.lambPosition) - transform.position);

        //        if (random < .5f)
        //        {
        //            // Use Magic Attack
        //            shootTimerLeft = shootTimer;
        //            GameObject attack = Instantiate(magicAttack);
        //            attack.transform.position = transform.position;
        //        }
        //        else if(hitLamb.collider.CompareTag("Lamb"))
        //        {
        //            // Barricade Player and Lamb
        //            shootTimerLeft = shootTimer;
        //            GameObject barricade = Instantiate(barricadeAttack);
        //        }

        //    }
        //}
        //else if(!hit.collider.CompareTag("Player"))
        //{
        //    if (right)
        //    {
        //        Vector3 rotated = Quaternion.AngleAxis(90, Vector3.up) * directionTowardsPlayer;
        //        transform.position += rotated.normalized * speed * Time.deltaTime;
        //    }
        //}
        //else if(distanceToPlayer < (targetDistance - variance))
        //{
        //    // Move away from player
        //    transform.position += -directionTowardsPlayer.normalized * speed * Time.deltaTime;
        //}
        //else if(distanceToPlayer > (targetDistance + variance))
        //{
        //    // Move towards player
        //    transform.position += directionTowardsPlayer.normalized * speed * Time.deltaTime;
        //}

    }

    /*void CheckFootstep()
    {
        if (footstepTimer <= 0)
        {
            footstepTimer = footstepTime;
            soundPlayer.PlaySound();
        }
    }*/

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Terrain"))
        {
            if (right)
            {
                right = false;
            }
            else
            {
                right = true;
            }
        }
    }
}
