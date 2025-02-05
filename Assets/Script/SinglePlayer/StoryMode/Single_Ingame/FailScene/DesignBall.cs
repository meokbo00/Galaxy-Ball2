using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingSphere : MonoBehaviour
{
    public float speed = 200f;
    public float moveDuration = 0.1f; // 이동 시간
    private Vector2 moveDirection;
    private float moveTimer = 0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomDirection();
    }

    private void Update()
    {
        moveTimer += Time.deltaTime;

        // Move the Rigidbody
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);

        if (moveTimer >= moveDuration)
        {
            SetRandomDirection();
            moveTimer = 0f;
        }
    }

    private void OnMouseDown()
    {
        // Optionally implement an async scene loading here if needed
        SceneManager.LoadScene("Start Scene");
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
    }
}
