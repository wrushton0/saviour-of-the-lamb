using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWhenDestroyed : MonoBehaviour
{
    public List<GameObject> activateThese;

    private void OnDestroy()
    {
        foreach(GameObject activateThis in activateThese)
            if (activateThis != null)
                activateThis.SetActive(true);
    }
}
