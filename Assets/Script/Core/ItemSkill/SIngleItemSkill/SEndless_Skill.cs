using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEndless_Skill : MonoBehaviour
{
    BGMControl bGMControl;
    private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void Start()
    {
        bGMControl = FindAnyObjectByType<BGMControl>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = SPGameManager.shotDirection * 3.5f;

        // Rigidbody2D 보간(interpolation) 설정을 통해 더 부드러운 이동 적용
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        // 물리 업데이트를 60FPS로 설정하여 물리 연산의 빈도를 줄임
        Time.fixedDeltaTime = 1f / 60f;
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (bGMControl.SoundEffectSwitch)
        {
            bGMControl.SoundEffectPlay(0);
        }
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectDirection = Vector2.Reflect(lastVelocity.normalized, normal);

        rb.velocity = reflectDirection * 3.5f;
    }
}
