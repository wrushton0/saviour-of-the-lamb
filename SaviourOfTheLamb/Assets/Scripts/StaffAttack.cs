using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttack : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSwing()
    {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void ReturnToNormal()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
