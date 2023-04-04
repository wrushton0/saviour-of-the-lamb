using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPointer : MonoBehaviour
{

    Vector3 directionToGoal;
    public float distance;
    public float tLerp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        directionToGoal = StaticScript.goalPosition - (Vector3) StaticScript.playerPosition;
        float angle = Mathf.Atan2(directionToGoal.y, directionToGoal.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 goalPosition = (Vector3)StaticScript.playerPosition + (directionToGoal.normalized * distance);

        transform.position = Vector3.Lerp(transform.position, goalPosition, tLerp);
    }
}
