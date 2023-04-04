using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowCultist : MonoBehaviour
{
    public bool moving, bounceOffOtherCultists;
    public float slowMoveSpeed, fastMoveSpeed, moveSpeed, moveWaitTimer, shootTimer, boltForce, boltOffset, crossbowOffset, stillShotVariance, movingShotVariance, moveTowardsPlayerDirectionVariance, playerSeenRecently, playerSightMaxTime, predictiveAimingChance/*, footstepTimer, slowFootstepTimer, fastFootstepTimer*/;
    public Vector2 waitTimeRange, moveTimeRange, shootTimeRange, moveDirection;
    public SpriteRenderer crossbowSprite;
    public GameObject bolt, crossbow, shootSound;
    //SoundPlayer soundPlayer;

    SpriteRenderer cultistSprite;

    // Start is called before the first frame update
    void Start()
    {
        shootTimer = Random.Range(shootTimeRange.x, shootTimeRange.y);
        cultistSprite = GetComponent<SpriteRenderer>();
        UpdateSprites();
        moveSpeed = slowMoveSpeed; // Start with the slow move speed.
        //soundPlayer = GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveWaitTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;

        /*footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0 && moving)
        {
            if (playerSeenRecently > 0)
                footstepTimer = fastFootstepTimer;
            else
                footstepTimer = slowFootstepTimer;

            soundPlayer.PlaySound();
        }*/

        // Moving and waiting.
        if (moving)
            transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * moveSpeed * Time.deltaTime;

        if (moveWaitTimer <= 0)
        {
            if (!moving)
            {
                Vector2 directionToPlayer = new Vector2(StaticScript.playerPosition.x - transform.position.x, StaticScript.playerPosition.y - transform.position.y);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer);
                //Debug.DrawRay(transform.position, directionToPlayer, Color.red, 1f);
                //print(hit.collider.tag);

                if (hit.collider.CompareTag("Player"))
                {
                    moveDirection = directionToPlayer;
                    moveDirection.Normalize();
                    moveDirection.x += Random.Range(-moveTowardsPlayerDirectionVariance, moveTowardsPlayerDirectionVariance);
                    moveDirection.y += Random.Range(-moveTowardsPlayerDirectionVariance, moveTowardsPlayerDirectionVariance);
                }
                else
                {
                    moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                }

                moveDirection.Normalize();
                moveWaitTimer = Random.Range(moveTimeRange.x, moveTimeRange.y);
            }
            else
            {
                moveWaitTimer = Random.Range(waitTimeRange.x, waitTimeRange.y);
            }
            
            moving = !moving;
        }

        CheckIfPlayerInSight();

        // Shooting.
        if (shootTimer <= 0)
        {
            Vector2 directionTowardsPlayer = new Vector2(StaticScript.playerPosition.x - transform.position.x, StaticScript.playerPosition.y - transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionTowardsPlayer);

            if (hit.collider.CompareTag("Player"))
            {
                GameObject newBolt = Instantiate(bolt, transform.position, transform.rotation);
                Instantiate(shootSound, transform.position, transform.rotation);
                Vector2 directionToPlayer = new Vector2(transform.position.x, transform.position.y) - StaticScript.playerPosition;

                Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                inputVector.Normalize();
                inputVector *= StaticScript.playerMoveSpeed;

                if (inputVector != Vector2.zero && Random.Range(0f, 1f) < predictiveAimingChance)
                {
                    /*Vector2 futurePlayerPosition = StaticScript.playerPosition + new Vector2(0, StaticScript.playerMoveSpeed);
                    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                    
                    float timeToTravel = Vector2.Distance(transform.position, futurePlayerPosition);*/

                    // The Rushton Predictive Aiming Algorithm
                    float velocity = boltForce / bolt.GetComponent<Rigidbody2D>().mass * Time.fixedDeltaTime;
                    float time = Vector2.Distance(transform.position, StaticScript.playerPosition) / velocity;
                    Vector2 futurePlayerPosition = StaticScript.playerPosition + inputVector * time;

                    for (int i = 0; i < 100; i++)
                    {
                        time = Vector2.Distance(transform.position, futurePlayerPosition) / velocity;
                        futurePlayerPosition = StaticScript.playerPosition + inputVector * time;
                    }

                    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                }

                //if (Input.GetKey("s"))
                //{
                //    /*Vector2 futurePlayerPosition = StaticScript.playerPosition + new Vector2(0, StaticScript.playerMoveSpeed);
                //    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                    
                //    float timeToTravel = Vector2.Distance(transform.position, futurePlayerPosition);*/

                //    // The Rushton Predictive Aiming Algorithm
                //    float velocity = boltForce / bolt.GetComponent<Rigidbody2D>().mass * Time.fixedDeltaTime;
                //    float time = Vector2.Distance(transform.position, StaticScript.playerPosition) / velocity;
                //    Vector2 futurePlayerPosition = StaticScript.playerPosition + new Vector2(0, -StaticScript.playerMoveSpeed) * time;

                //    for (int i = 0; i < 1000; i++)
                //    {
                //        time = Vector2.Distance(transform.position, futurePlayerPosition) / velocity;
                //        futurePlayerPosition = StaticScript.playerPosition + new Vector2(0, -StaticScript.playerMoveSpeed) * time;
                //    }

                //    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                //}

                //if (Input.GetKey("a"))
                //{
                //    /*Vector2 futurePlayerPosition = StaticScript.playerPosition + new Vector2(0, StaticScript.playerMoveSpeed);
                //    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                    
                //    float timeToTravel = Vector2.Distance(transform.position, futurePlayerPosition);*/

                //    // The Rushton Predictive Aiming Algorithm
                //    float velocity = boltForce / bolt.GetComponent<Rigidbody2D>().mass * Time.fixedDeltaTime;
                //    float time = Vector2.Distance(transform.position, StaticScript.playerPosition) / velocity;
                //    Vector2 futurePlayerPosition = StaticScript.playerPosition + new Vector2(-StaticScript.playerMoveSpeed, 0) * time;

                //    for (int i = 0; i < 1000; i++)
                //    {
                //        time = Vector2.Distance(transform.position, futurePlayerPosition) / velocity;
                //        futurePlayerPosition = StaticScript.playerPosition + new Vector2(-StaticScript.playerMoveSpeed, 0) * time;
                //    }

                //    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                //}

                //if (Input.GetKey("a"))
                //{
                //    /*Vector2 futurePlayerPosition = StaticScript.playerPosition + new Vector2(0, StaticScript.playerMoveSpeed);
                //    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                    
                //    float timeToTravel = Vector2.Distance(transform.position, futurePlayerPosition);*/

                //    // The Rushton Predictive Aiming Algorithm
                //    float velocity = boltForce / bolt.GetComponent<Rigidbody2D>().mass * Time.fixedDeltaTime;
                //    float time = Vector2.Distance(transform.position, StaticScript.playerPosition) / velocity;
                //    Vector2 futurePlayerPosition = StaticScript.playerPosition + new Vector2(StaticScript.playerMoveSpeed, 0) * time;

                //    for (int i = 0; i < 1000; i++)
                //    {
                //        time = Vector2.Distance(transform.position, futurePlayerPosition) / velocity;
                //        futurePlayerPosition = StaticScript.playerPosition + new Vector2(StaticScript.playerMoveSpeed, 0) * time;
                //    }

                //    directionToPlayer = new Vector2(transform.position.x, transform.position.y) - futurePlayerPosition;
                //}

                directionToPlayer.Normalize(); // Normalise before varying angle (so the variance has an equal effect each time).

                // Make shot inaccurate.
                if (moving)
                    directionToPlayer += new Vector2(Random.Range(-movingShotVariance, movingShotVariance), Random.Range(-movingShotVariance, movingShotVariance));
                else
                    directionToPlayer += new Vector2(Random.Range(-stillShotVariance, stillShotVariance), Random.Range(-stillShotVariance, stillShotVariance));

                directionToPlayer.Normalize(); // Normalise again after varying angle (because the magnitude likely won't still be 1).
                float rotation_z = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                newBolt.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + boltOffset);

                // Crossbow changes.
                crossbow.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + crossbowOffset);
                UpdateSprites();

                newBolt.GetComponent<Rigidbody2D>().AddForce(-1 * directionToPlayer * boltForce);
            }

            shootTimer = Random.Range(shootTimeRange.x, shootTimeRange.y);
        }
    }

    void UpdateSprites()
    {
        float zAngle = crossbow.transform.rotation.eulerAngles.z;

        if (zAngle > 0 && zAngle <= 90)
        {
            crossbowSprite.sortingLayerName = "Weapon Below Cultist";
            cultistSprite.flipX = false;
        }
        else if (zAngle > 90 && zAngle <= 180)
        {
            crossbowSprite.sortingLayerName = "Weapon Below Cultist";
            cultistSprite.flipX = true;
        }
        else if (zAngle > 180 && zAngle <= 270)
        {
            crossbowSprite.sortingLayerName = "Weapon Above Cultist";
            cultistSprite.flipX = true;
        }
        else
        {
            crossbowSprite.sortingLayerName = "Weapon Above Cultist";
            cultistSprite.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Terrain") || (col.collider.CompareTag("Cultist") && bounceOffOtherCultists) || col.collider.CompareTag("Player"))
        {
            moveDirection.x *= -1;
            moveDirection.y *= -1;

            // Reverse random components of the cultist's moveDirection vector.
            // Buggy because if they choose to move towards the terrain, they won't reroll this chance because the collision doesn't get detected again.
            // This makes the cultist rub against the wall.
            /*print("hit terrain");
            int option = Random.Range(1,5);

            if (option == 1)
                moveDirection.x *= -1;
            else if (option == 2)
                moveDirection.y *= -1;
            else
            {
                // Double the chance to get here.
                moveDirection.x *= -1;
                moveDirection.y *= -1;
            }*/
        }

        else if (col.collider.CompareTag("Cultist"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
        }
    }

    void CheckIfPlayerInSight()
    {
        Vector2 directionTowardsPlayer = new Vector2(StaticScript.playerPosition.x - transform.position.x, StaticScript.playerPosition.y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionTowardsPlayer);

        if (hit.collider.CompareTag("Player"))
        {
            playerSeenRecently = playerSightMaxTime;
            moveSpeed = fastMoveSpeed;
        }
        else
        {
            if (playerSeenRecently > 0)
            {
                playerSeenRecently -= Time.deltaTime;

                if (playerSeenRecently <= 0)
                {
                    moveSpeed = slowMoveSpeed;
                }
            }
        }
    }
}
