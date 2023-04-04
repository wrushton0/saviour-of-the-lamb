using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLambAI : MonoBehaviour
{
    public float followDistance, speed, wanderSpeed, variance, wanderTimer, moveForTimer, moveForVariance, wanderTimerVariance ;
    float distanceToPlayer, wanderTimerLeft, moveForTimerLeft;
    Vector3 directionToPlayer;
    Vector2 wanderDirection;
    bool goToPlayer;
    SpriteRenderer spriteRenderer;

    Animator anim;
    bool lambIsMoving;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        wanderTimerLeft -= Time.deltaTime;
        moveForTimerLeft -= Time.deltaTime;

        directionToPlayer = (Vector3) StaticScript.playerPosition - transform.position;
        distanceToPlayer = Vector3.Distance(StaticScript.playerPosition, transform.position);

        if (moveForTimerLeft < 0)
        {
            anim.SetBool("isWandering", false);
        }

        if (distanceToPlayer > followDistance + variance)
        {
            goToPlayer = true;
            anim.SetBool("isMoving", true);
            anim.SetBool("isWandering", false);
        }
        else if (distanceToPlayer < followDistance)
        {
            goToPlayer = false;
            anim.SetBool("isMoving", false);
        }

        if (goToPlayer)
        {


            
            transform.position += directionToPlayer.normalized * speed * Time.deltaTime;

            if(directionToPlayer.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            if(directionToPlayer.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        if (!goToPlayer)
        {
            if(wanderTimerLeft < 0)
            {
                wanderTimerLeft = Random.Range(wanderTimer - wanderTimerVariance, wanderTimer + wanderTimerVariance);

                moveForTimerLeft = Random.Range(moveForTimer - moveForVariance, moveForTimer + moveForVariance);

                wanderDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }

            if(moveForTimerLeft > 0)
            {
                anim.SetBool("isWandering", true);
                transform.position += (Vector3) wanderDirection.normalized * wanderSpeed * Time.deltaTime;
                if (wanderDirection.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                if (wanderDirection.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }

        StaticScript.lambPosition = transform.position;

    }
}
