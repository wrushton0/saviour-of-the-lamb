using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1GoalPointerPatch : MonoBehaviour
{
    public Transform endLevel;

    // Start is called before the first frame update
    void Start()
    {
        StaticScript.goalPosition = endLevel.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
