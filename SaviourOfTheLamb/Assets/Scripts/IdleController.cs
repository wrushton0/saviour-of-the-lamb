using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleController : MonoBehaviour
{
    public float idleSpeed;
    public Vector2 waitTimeRange, moveTimeRange;

    Vector3 idleMoveDirection;
    bool moving;
    public float idleTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Terrain"))
        {
            idleMoveDirection.x *= -1;
            idleMoveDirection.y *= -1;
        }
    }
}
