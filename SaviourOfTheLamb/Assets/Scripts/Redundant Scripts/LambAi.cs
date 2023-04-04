using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambAi : MonoBehaviour
{
    // minDis is the distance at which the lambs movement is completely random.
    // maxDis is the distance at which the lambs movement is always towards the player.
    public float minDis;
    public float maxDis;

    public float lerpT;

    // disDiv is the divisor to the distance to produce a probability.
    float disDiv;

    // moveTimer is the time between lamb movements.
    // moveTimerLeft is the remaining time for each movement
    public float moveTimer;
    float moveTimerLeft;

    Vector3 target;

    public float speed;

    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        // Sets moveTimerLeft so lamb doesnt move isntantly.
        moveTimerLeft = moveTimer;
        // Calculates the distance divisor for saving calculations.
        disDiv = (maxDis - minDis) * 2;

        // Sets the render component for access.
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimerLeft -= Time.deltaTime;



        if (moveTimerLeft <= 0)
        {
            moveTimerLeft = moveTimer;
            float dis = Vector3.Distance(StaticScript.playerPosition, transform.position);
            float towardsProb = .0f + (dis / disDiv);

            //float towardProb = 0.5f + (dis / disDiv);

            Vector3 direction = (transform.position - (Vector3) StaticScript.playerPosition).normalized;

            float randomx = Random.Range(-speed, speed);
            float randomy = Random.Range(-speed, speed);

            direction += direction + (new Vector3(randomx, randomy, 0)).normalized;

            float rand = Random.Range(0f, 1f);

            if (rand < towardsProb)
            {
                target = transform.position + -direction * speed;
            }
            else
            {
                target = transform.position + direction * speed;

            }

            Vector3 checkDirectionVector = transform.position - target;

            if(checkDirectionVector.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        Vector2 lerpPosition = Vector2.Lerp(transform.position, target, lerpT); // May need to use Time.deltaTime here.
        transform.position = new Vector3(lerpPosition.x, lerpPosition.y, 0);

        StaticScript.lambPosition = transform.position;
    }

    private void OnEnable()
    {
        target = transform.position;
        moveTimerLeft = 0.3f;
    }
}
