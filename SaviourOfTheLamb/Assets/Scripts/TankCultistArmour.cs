using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCultistArmour : MonoBehaviour
{
    public float maxHealth;
    public List<GameObject> armourPieces;

    public void UpdateArmour(float curHealth)
    {
        if (curHealth / maxHealth < 0.97f) // Basically full health.
            armourPieces[0].SetActive(false);
        if (curHealth / maxHealth < 0.9f)
            armourPieces[1].SetActive(false);
        if (curHealth / maxHealth < 0.8f)
            armourPieces[2].SetActive(false);
        if (curHealth / maxHealth < 0.7f)
            armourPieces[3].SetActive(false);
        if (curHealth / maxHealth < 0.6f)
            armourPieces[4].SetActive(false);
        if (curHealth / maxHealth < 0.5f)
            armourPieces[5].SetActive(false);
        if (curHealth / maxHealth < 0.4f)
            armourPieces[6].SetActive(false);
        if (curHealth / maxHealth < 0.3f)
            armourPieces[7].SetActive(false);
        if (curHealth / maxHealth < 0.2f)
            armourPieces[8].SetActive(false);
        if (curHealth / maxHealth < 0.1f)
            armourPieces[9].SetActive(false);
    }

    public void UpdateSprites()
    {
        // Player on right.
        if (StaticScript.playerPosition.x > transform.position.x)
        {
            foreach(GameObject armour in armourPieces)
                armour.GetComponent<SpriteRenderer>().flipX = false;
        }

        // Player on right.
        else
        {
            foreach(GameObject armour in armourPieces)
                armour.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
