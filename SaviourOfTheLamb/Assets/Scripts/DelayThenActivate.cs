using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayThenActivate : MonoBehaviour
{
    public float timer;
    public MonoBehaviour scriptToActivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            scriptToActivate.enabled = true;
            Destroy(this);
        }
    }
}
