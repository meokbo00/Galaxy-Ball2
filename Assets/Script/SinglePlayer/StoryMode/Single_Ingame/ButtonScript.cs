using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float colorChangeAmount = 60f / 255f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnMouseDown()
    {
        spriteRenderer.color = new Color(
            Mathf.Max(originalColor.r - colorChangeAmount, 0),
            Mathf.Max(originalColor.g - colorChangeAmount, 0),
            Mathf.Max(originalColor.b - colorChangeAmount, 0),
            originalColor.a
        );
    }

    void OnMouseUp()
    {
        spriteRenderer.color = originalColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }
}