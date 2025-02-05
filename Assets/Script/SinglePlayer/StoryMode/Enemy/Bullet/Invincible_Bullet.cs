using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invincible_Bullet : MonoBehaviour
{
    SPGameManager spGameManager;
    Rigidbody2D rb;
    BGMControl bGMControl;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public float increase;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private int durability;
    public PhysicsMaterial2D bouncyMaterial;
    private Vector3 initialScale; // 초기 공 크기
    private Vector3 targetScale; // 목표 크기
    private const string GojungTag = "Gojung";
    private const string WallTag = "Wall";
    private const string EnemyCenterTag = "EnemyCenter";



    private void Start()
    {
        spGameManager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        rb = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");

        rb.drag = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && bouncyMaterial != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }

        initialScale = transform.localScale;
    }

    private void Update()
    {
        Move();
        expand();
    }

    void Move()
    {
        if (rb == null || isStopped) return;

        lastVelocity = rb.velocity;
        rb.velocity -= rb.velocity.normalized * deceleration * Time.deltaTime;

        if (rb.velocity.magnitude <= 0.01f && hasExpanded)
        {
            isStopped = true;
        }
    }

    void expand()
    {
        if (rb == null || iscolliding) return;
        if (rb.velocity.magnitude > 0.1f) return;
        if (Input.GetMouseButton(0)) return;

        if (!hasExpanded && bGMControl.SoundEffectSwitch)
        {
            bGMControl.SoundEffectPlay(1);
        }
        transform.localScale += Vector3.one * increase * Time.deltaTime;
        hasExpanded = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!hasExpanded && bGMControl.SoundEffectSwitch)
        {
            bGMControl.SoundEffectPlay(0);
        }
        if (!coll.collider.isTrigger && hasExpanded)
        {
            hasExpanded = true; // 팽창 중단
            transform.localScale = transform.localScale; // 현재 크기에서 멈춤
            DestroyRigidbody(); // Rigidbody 제거
        }
        if (!coll.collider.CompareTag(WallTag))
        {
            if(coll.collider.tag == "EnemyBall" || coll.collider.tag == "P1ball")
            {
                spGameManager.RemoveBall();
                Destroy(coll.gameObject);
            }
            else if(coll.collider.tag == "EnemyCenter")
            {
                spGameManager.RemoveEnemy();
                Destroy(coll.gameObject);
            }
            else
            {
                Destroy(coll.gameObject);
            }
            Destroy(gameObject);
            spGameManager.RemoveBall();
        }
        this.iscolliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.iscolliding = false;
    }
    void DestroyRigidbody()
    {
        if (rb != null)
        {
            Destroy(rb);
            rb = null;
        }
    }
}