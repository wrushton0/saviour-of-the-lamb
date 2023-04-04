using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : MonoBehaviour
{
    public float lerpT, screenShakeForce, boltShootForce, reloadTime, reloadTimer, shotVariance;
    public Transform moveTo, returnTo;
    public Sprite beforeShot, afterShot;
    public GameObject mainCamera, bolt, shootSound, crossbow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, returnTo.position, lerpT);
        
        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0)
                crossbow.GetComponent<SpriteRenderer>().sprite = beforeShot;
        }
            

        if (Input.GetMouseButtonDown(0) && reloadTimer <= 0)
        {
            GameObject newBolt = Instantiate(bolt, transform.position, transform.rotation);
            
            Vector3 newRotation = newBolt.transform.right + new Vector3(Random.Range(-shotVariance, shotVariance), Random.Range(-shotVariance, shotVariance), 0);
            float rotation_z = Mathf.Atan2(newRotation.y, newRotation.x) * Mathf.Rad2Deg;
            newBolt.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
            
            newBolt.GetComponent<Rigidbody2D>().AddForce(newBolt.transform.right * boltShootForce);
            transform.position = moveTo.position;
            mainCamera.GetComponent<CameraController>().ShakeScreen(transform.right, screenShakeForce);
            reloadTimer = reloadTime;

            Instantiate(shootSound, transform.position, transform.rotation);
            crossbow.GetComponent<SpriteRenderer>().sprite = afterShot;
        }
    }
}
