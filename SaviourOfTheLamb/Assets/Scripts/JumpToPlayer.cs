using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticScript.player != null)
            transform.position = StaticScript.playerPosition;
    }
}
