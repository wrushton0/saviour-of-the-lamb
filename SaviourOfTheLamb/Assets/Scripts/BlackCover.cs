using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackCover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StaticScript.blackCover = GetComponent<Image>();
    }
}
