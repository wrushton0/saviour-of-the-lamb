using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticScript : MonoBehaviour
{
    public static int aimSector, cultistCount, cultistsKilled;
    public static float playerMoveSpeed;
    public static Vector2 playerPosition;
    public static Vector2 lambPosition;
    public static GameObject mainCamera, player;
    public static bool isMelee, inTextScene, usingCrossbow;
    public static bool isBarricade, ambienceExists;
    public static Vector3 goalPosition;
    public static Image blackCover;
}
