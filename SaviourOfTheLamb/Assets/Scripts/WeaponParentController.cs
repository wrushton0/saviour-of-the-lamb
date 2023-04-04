using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParentController : MonoBehaviour
{
    public bool hasCrossbow;
    public float offset;
    public SpriteRenderer crossbowSprite, woolShearsSprite;
    public GameObject crossbow, woolShears;
    
    // Start is called before the first frame update
    void Start()
    {
        if (hasCrossbow)
            StaticScript.usingCrossbow = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Point at mouse cursor.
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);

        if (Input.GetKeyDown("space"))
        {
            if (hasCrossbow && crossbow.activeSelf)
            {
                crossbow.SetActive(false);
                woolShears.SetActive(true);
                StaticScript.usingCrossbow = false;
                StartCoroutine(ZoomCamera(8f, 6.67f, .15f, 7f));
            }
            else if (hasCrossbow)
            {
                crossbow.SetActive(true);
                woolShears.SetActive(false);
                StaticScript.usingCrossbow = true;
                StartCoroutine(ZoomCamera(6.67f, 8f, .15f, 7f));
            }
        }

        float zAngle = transform.rotation.eulerAngles.z;

        if (zAngle > 0 && zAngle <= 90)
            StaticScript.aimSector = 1;
        else if (zAngle > 90 && zAngle <= 180)
            StaticScript.aimSector = 2;
        else if (zAngle > 180 && zAngle <= 270)
            StaticScript.aimSector = 3;
        else
            StaticScript.aimSector = 4;

        if (StaticScript.aimSector == 1 || StaticScript.aimSector == 2)
        {
            if (hasCrossbow && crossbow.activeSelf)
                crossbowSprite.sortingLayerName = "Player Weapon Below";
            else
                woolShearsSprite.sortingLayerName = "Player Weapon Below";
        }
        else
        {
            if (hasCrossbow && crossbow.activeSelf)
                crossbowSprite.sortingLayerName = "Player Weapon Above";
            else
                woolShearsSprite.sortingLayerName = "Player Weapon Above";
        }
    }

    IEnumerator ZoomCamera(float from, float to, float time, float steps)
    {
        float f = 0;

        while (f <= 1)
        {
            Camera.main.gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(from, to, f);

            f += 1f / steps;

            yield return new WaitForSeconds(time / steps);
        }
    }

}
