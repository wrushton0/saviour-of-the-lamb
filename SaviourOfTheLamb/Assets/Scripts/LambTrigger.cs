using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambTrigger : MonoBehaviour
{

    public GameObject lamb;
    public GameObject allowNextLevel;
    public List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StaticScript.goalPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lamb.SetActive(true);
            allowNextLevel.SetActive(true);
            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            transform.position = new Vector2(-10,1);
        }
    }
}
