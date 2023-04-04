using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speedMultiplier = 1f;
    public Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveVector * Time.deltaTime * speedMultiplier;
    }
}
