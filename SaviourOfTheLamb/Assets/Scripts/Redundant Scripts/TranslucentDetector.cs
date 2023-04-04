using UnityEngine;

public class TranslucentDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Translucent"))
        {
            Debug.Log("Entered an Translucent region!");
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Translucent"))
        {
            Debug.Log("Exited an Translucent region!");
        }
    }
}