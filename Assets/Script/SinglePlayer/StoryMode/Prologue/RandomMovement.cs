using UnityEngine;

public class ContinuousRandomMovement : MonoBehaviour
{
    private Vector2 moveDirection;  
    private Rigidbody2D rb;       
    public float moveSpeed;       
    public PhysicsMaterial2D bouncyMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector2(Random.Range(-199, 199), Random.Range(-190, 190));
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        moveDirection.Normalize();  // 방향 벡터를 정규화
        moveSpeed = Random.Range(2, 17);

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && bouncyMaterial != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
}
