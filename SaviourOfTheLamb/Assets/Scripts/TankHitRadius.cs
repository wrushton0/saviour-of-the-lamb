using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHitRadius : MonoBehaviour
{
    public TankController tankController;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            tankController.playerInHitRadius = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            tankController.playerInHitRadius = false;
    }
}
