using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float idleSpeed, chaseSpeed, damage, hitTime, swingTime;
    public Vector2 waitTimeRange, moveTimeRange;
    public bool playerInHitRadius;
    
    Vector3 idleMoveDirection;
    bool moving;
    public float idleTimer, hitTimer;
    TankCultistArmour armourScript;
    StaffAttack staffScript;
    float swingTimer;

    // Start is called before the first frame update
    void Start()
    {
        armourScript = GetComponent<TankCultistArmour>();
        staffScript = GetComponentInChildren<StaffAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDirection = new Vector2(StaticScript.playerPosition.x - transform.position.x, StaticScript.playerPosition.y - transform.position.y);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, GetComponent<CircleCollider2D>().radius, playerDirection);
        armourScript.UpdateSprites();

        if (hit.collider.CompareTag("Player"))
            transform.position += new Vector3(playerDirection.normalized.x, playerDirection.normalized.y, 0) * chaseSpeed * Time.deltaTime;
        else
        {
            if (moving)
                transform.position += idleMoveDirection * idleSpeed * Time.deltaTime;

            idleTimer -= Time.deltaTime;

            if (idleTimer <= 0)
            {
                if (!moving)
                {
                    idleMoveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                    idleTimer = Random.Range(moveTimeRange.x, moveTimeRange.y);
                    moving = true;
                }
                else
                {
                    idleTimer = Random.Range(waitTimeRange.x, waitTimeRange.y);
                    moving = false;
                }
            }
        }

        if (playerInHitRadius)
        {
            hitTimer -= Time.deltaTime;

            if (hitTimer <= 0)
            {
                StaticScript.player.GetComponent<PlayerController>().DamagePlayer(damage);
                hitTimer = hitTime;
                AnimateSwing();
            }
        }
        else
            hitTimer = hitTime;

        if (swingTimer > 0)
        {
            swingTimer -= Time.deltaTime;
            staffScript.StartSwing();

            if (swingTimer <= 0)
            {
                staffScript.ReturnToNormal();
                staffScript.enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Terrain"))
        {
            idleMoveDirection.x *= -1;
            idleMoveDirection.y *= -1;
        }
    }

    void AnimateSwing()
    {
        swingTimer = swingTime;
    }
}
