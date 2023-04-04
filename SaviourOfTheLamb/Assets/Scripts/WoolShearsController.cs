using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolShearsController : MonoBehaviour
{
    public float lerpT, screenShakeForce, reloadTime, reloadTimer, appearTimer, appearTimerLeft, speed;
    public Transform moveTo, returnTo;
    public GameObject mainCamera, woolShears, woolShearsAttack;
    bool setComponent;
    WoolShearsAttack setDamage;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.gameObject;
        setDamage = woolShearsAttack.GetComponent<WoolShearsAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!setComponent)
        {
            setDamage = woolShearsAttack.GetComponent<WoolShearsAttack>();
        }
        if (reloadTimer > 0)
            reloadTimer -= Time.deltaTime;

        if(appearTimerLeft > 0)
        {
            appearTimerLeft -= Time.deltaTime;
        }

        if(appearTimerLeft < 0)
        {
            woolShearsAttack.SetActive(false);
            woolShears.SetActive(true);
            StaticScript.isMelee = false;
            setDamage.damage = 3;
        }
        if(appearTimerLeft > 0)
        {
            Vector3 directionToPlayer = (Vector3) StaticScript.playerPosition - woolShearsAttack.transform.position;
            directionToPlayer = new Vector2(directionToPlayer.y, -directionToPlayer.x);
            woolShearsAttack.transform.position += directionToPlayer * speed * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && reloadTimer <= 0)
        {
            StaticScript.isMelee = true;
            reloadTimer = reloadTime;
            appearTimerLeft = appearTimer;
            woolShears.SetActive(false);
            woolShearsAttack.SetActive(true);
            woolShearsAttack.transform.position = woolShears.transform.position;
            Vector2 angleToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            mainCamera.GetComponent<CameraController>().ShakeScreen(angleToMouse, screenShakeForce);
        }
    }
}