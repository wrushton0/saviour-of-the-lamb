using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerPickupCrossbow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponentInChildren<WeaponParentController>().woolShears.SetActive(false);
            col.gameObject.GetComponentInChildren<WeaponParentController>().crossbow.SetActive(true);
            Destroy(gameObject);
        }
    }
}
