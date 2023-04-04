using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnatcherCultist : MonoBehaviour
{
    public bool hasLamb;
    public float wanderSpeed, speed, exitSpeed, radius/*, footstepTimer, walkFootstepTime, chaseFootstepTime, exitFootstepTime*/;
    public Vector2 exitDirection, moveDirection;
    //SoundPlayer soundPlayer;
    public GameObject lamb, baSound;
    SnatcherStealsLamb lambStolenScript;

    bool chasingLamb;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        moveDirection.Normalize();
        //soundPlayer = GetComponent<SoundPlayer>();
        lambStolenScript = GetComponent<SnatcherStealsLamb>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLamb)
        {
            Vector2 directionTowardsLamb = new Vector2(StaticScript.lambPosition.x - transform.position.x, StaticScript.lambPosition.y - transform.position.y);
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, GetComponent<CircleCollider2D>().radius * 0.7f, directionTowardsLamb); // * 0.7f because otherwise the circlecast will pick up the wall the cultist is rubbing against

            if (hit.collider.CompareTag("Lamb"))
            {
                transform.position += new Vector3(directionTowardsLamb.normalized.x, directionTowardsLamb.normalized.y, 0) * speed * Time.deltaTime;
                chasingLamb = true;
            }
            else
            {
                // Was chasing the lamb but can no longer see it.
                if (chasingLamb)
                {
                    chasingLamb = false;
                    moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    moveDirection.Normalize();
                }

                transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * wanderSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (exitDirection == Vector2.zero)
            {
                FindRunDirection(10);
            }

            transform.position += new Vector3(exitDirection.x, exitDirection.y, 0) * exitSpeed * Time.deltaTime;
            lamb.transform.position = transform.position;
        }

        // Footstep sounds
        /*footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            soundPlayer.PlaySound();

            if (hasLamb)
                footstepTimer = exitFootstepTime;
            else if (chasingLamb)
                footstepTimer = chaseFootstepTime;
            else
                footstepTimer = walkFootstepTime;
        }*/

        if (StaticScript.lambPosition.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else
            GetComponent<SpriteRenderer>().flipX = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Lamb"))
        {
            lamb = collision.gameObject;
            lamb.GetComponent<NewLambAI>().enabled = false;
            lamb.GetComponent<Collider2D>().enabled = false;
            hasLamb = true;

            Instantiate(baSound, transform.position, transform.rotation);

            FindRunDirection(2000);
        }

        if (collision.collider.CompareTag("Edge") && hasLamb)
        {
            lambStolenScript.enabled = true;
            GetComponent<Collider2D>().enabled = false;
        }

        if (collision.collider.CompareTag("Terrain") || (collision.collider.CompareTag("Edge") && !hasLamb))
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            moveDirection.Normalize();
        }
    }

    void FindRunDirection(float samples)
    {
        for (int i = 0; i < samples; i++)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //RaycastHit2D hit = Physics2D.CircleCast(transform.position, GetComponent<CircleCollider2D>().radius, randomDirection);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, randomDirection);
            if (hit.collider.CompareTag("Edge"))
            {
                exitDirection = randomDirection;
                exitDirection.Normalize();
                break;
            }
        }
    }
}
