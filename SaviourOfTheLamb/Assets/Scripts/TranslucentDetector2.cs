using UnityEngine;

public class TranslucentDetector2 : MonoBehaviour
{
    public float transparency;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, transparency);
                spriteRenderer.sortingLayerName = "Rock In Front of Player";
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                spriteRenderer.sortingLayerName = "Rock Behind Player";
            }
        }
    }
}