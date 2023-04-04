using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public GameObject transitionController;

    public float lambDistance;

    public bool lambExists;

    // Start is called before the first frame update
    void Start()
    {
        lambDistance = 3;
    }

    // Update is called once per frame
    void Update()
    {
        StaticScript.goalPosition = transform.position;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(Vector3.Distance(StaticScript.playerPosition, StaticScript.lambPosition) < lambDistance || !lambExists)
            {

            
                transitionController.SetActive(true);
                GameObject player = col.gameObject;
                player.GetComponent<PlayerController>().enabled = false;

            if (player.GetComponentInChildren<WeaponParentController>() != null)
                player.GetComponentInChildren<WeaponParentController>().gameObject.SetActive(false);
                
            }
        }
    }
}
