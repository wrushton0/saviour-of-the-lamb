using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambHealth : MonoBehaviour
{
    float maxHealth;
    public float health;

    public float regenTime;
    float regenTimeLeft;

    SpriteRenderer spriteRenderer;

    public Sprite damagedLamb;
    public Sprite healedLamb;
    float veryLargeNumber = 3600;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        regenTimeLeft = veryLargeNumber;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        regenTimeLeft -= Time.deltaTime;

        if(regenTimeLeft < 0)
        {
            health = maxHealth;
            spriteRenderer.sprite = healedLamb;
            regenTimeLeft = veryLargeNumber;
        }
    }

    public void DamageLamb(float damage)
    {
        health -= damage;
        regenTimeLeft = regenTime;
        if(health/maxHealth <= .5f)
        {
            spriteRenderer.sprite = damagedLamb;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            // Game Over.
        }
    }
}

