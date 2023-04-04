using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, basehealth, invincibleTime, invincibleTimer, moveSoundTime, dodgeSpeed, dodgeTimer, dodgeForTimer;
    float moveSoundTimer, dodgeTimerLeft, dodgeForTimerLeft;
    public SoundPlayer soundPlayer;
    public GameObject injuredSound;
    SpriteRenderer spriteRenderer;
    bool dodging;
    Vector3 dodgeDirection, moveVector;
    public GameObject weaponsParent, gore;
    Animator anim;
    public float regenTimer;
    float regenTimerLeft, health;

    public GameObject playerTomb;
    public GameObject playerDeadController;

    public bool damagedSprite;

    // Start is called before the first frame update
    void Start()
    {
        health = basehealth;
        regenTimerLeft = regenTimer;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StaticScript.playerMoveSpeed = moveSpeed;
        StaticScript.player = gameObject;
        StaticScript.playerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        invincibleTimer -= Time.deltaTime;
        moveSoundTimer -= Time.deltaTime;
        dodgeForTimerLeft -= Time.deltaTime;
        dodgeTimerLeft -= Time.deltaTime;
        regenTimerLeft -= Time.deltaTime;

        if(regenTimerLeft < 0)
        {
            health = basehealth;
            damagedSprite = false;
            anim.SetBool("idleHurt", false);
            anim.SetBool("movingHurt", false);
        }

        // Move player.
        if (!dodging)
        {
            anim.SetBool("playerMoving", true);
            moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0); // GetAxisRaw is binary and feels more responsive.
            moveVector.Normalize();
        }

        if (moveVector == Vector3.zero)
        {
            anim.SetBool("playerMoving", false);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && dodgeTimerLeft <= 0)
        {
            dodging = true;
            dodgeDirection = moveVector;
            dodgeForTimerLeft = dodgeForTimer;
            invincibleTimer = dodgeForTimer;
            dodgeTimerLeft = dodgeTimer;

            if (weaponsParent != null)
                weaponsParent.SetActive(false);
        }

        if (dodging)
        {
            transform.position += moveVector * dodgeSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += moveVector * moveSpeed * Time.deltaTime;

            StaticScript.playerPosition = transform.position;

            if (moveVector != Vector3.zero && moveSoundTimer <= 0)
            {
                moveSoundTimer = moveSoundTime;
                soundPlayer.PlaySound();
            }
        }

        if(dodgeForTimerLeft < 0)
        {
            dodging = false;

            if (weaponsParent != null)
                weaponsParent.SetActive(true);
        }

        if (StaticScript.aimSector == 1 || StaticScript.aimSector == 4)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    public void DamagePlayer(float damage)
    {
        if (invincibleTimer <= 0)
        {
            health -= damage;
            regenTimerLeft = regenTimer;
            invincibleTimer = invincibleTime;
            Instantiate(injuredSound, transform.position, transform.rotation);
            
            for (int i = 0; i < 3; i++)
                Instantiate(gore, transform.position, transform.rotation);
        }

        if (health / basehealth <= .4)
        {
            damagedSprite = true;
            anim.SetBool("idleHurt", true);
            anim.SetBool("movingHurt", true);
        }


        if (health <= 0)
        {
            Instantiate(playerTomb, transform.position, transform.rotation);
            playerDeadController.transform.position = transform.position;
            playerDeadController.SetActive(true);
            Destroy(gameObject);
        }
    }
}
