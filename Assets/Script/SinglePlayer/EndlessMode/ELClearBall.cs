using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ELClearBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    public float reflectionMultiplier = 5f;

    void Start()
    {
        float x = Random.Range(1, 5);
        float y = Random.Range(1, 5);
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(x, y) * 5;
    }

    void FixedUpdate()
    {
        // lastVelocity를 계속 업데이트하지 않고, 필요할 때만 업데이트
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        lastVelocity = rb.velocity; // 여기서 업데이트
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectDirection = Vector2.Reflect(lastVelocity.normalized, normal);

        rb.velocity = reflectDirection * reflectionMultiplier;
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene("EndlessInGame");
    }
}
