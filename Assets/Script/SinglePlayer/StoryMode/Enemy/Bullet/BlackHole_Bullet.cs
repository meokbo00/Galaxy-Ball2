using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole_Bullet : MonoBehaviour
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
    public PhysicsMaterial2D bouncyMaterial;
    private Vector3 initialScale; // 초기 공 크기
    private Vector3 targetScale; // 목표 크기
    private const string GojungTag = "Gojung";
    private const string WallTag = "Wall";
    private const string EnemyCenterTag = "EnemyCenter";
    public bool isExpanding = false; // 공이 팽창 중인지 여부
    private float expandSpeed = 1f; // 팽창 속도
    private float decelerationThreshold = 0.4f;
    private Vector3 velocity = Vector3.zero;
    public int PlusScale;


    private void Start()
    {
        spGameManager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        rb = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");
        StartCoroutine(DestroyObjectDelayed(Random.Range(15f, 25f)));

        textObject.transform.parent = transform;

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

    private void FixedUpdate()
    {
        if (!isStopped)
        {
            SlowDownBall();
        }
    }
    private void Update()
    {
        if (isExpanding)
        {
            ExpandBall();
        }
    }

    void SlowDownBall()
    {
        if (rb == null) return;

        rb.velocity *= 1f - (Time.deltaTime * (deceleration * 0.4f)); // drag 효과 줄이기
        if (rb.velocity.magnitude <= decelerationThreshold)
        {
            rb.velocity = Vector2.zero;
            isStopped = true;
            StartExpansion();
        }
    }

    void StartExpansion()
    {
        if (bGMControl.SoundEffectSwitch)
        {
            bGMControl.SoundEffectPlay(1);
        }
        targetScale = initialScale * PlusScale;
        isExpanding = true;
    }

    void ExpandBall()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, expandSpeed);

        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            transform.localScale = targetScale; // 목표 크기에 도달하면 팽창 완료
            isExpanding = false; // 팽창 중단
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isExpanding && bGMControl.SoundEffectSwitch)
        {
            bGMControl.SoundEffectPlay(0);
        }
        if (!collision.collider.isTrigger && isExpanding)
        {
            isExpanding = false; // 팽창 중단
            transform.localScale = transform.localScale; // 현재 크기에서 멈춤
            DestroyRigidbody(); // Rigidbody 제거
        }

        if ((!collision.collider.CompareTag(GojungTag) && rb == null))
        {
            if (collision.gameObject.tag == WallTag) return;
            if (collision.gameObject.tag == EnemyCenterTag) return;

            spGameManager.RemoveBall();
            Destroy(collision.gameObject);
        }
        this.iscolliding = true;
    }
    void DestroyRigidbody()
    {
        if (rb != null)
        {
            Destroy(rb);
            rb = null;
        }
    }
    IEnumerator DestroyObjectDelayed(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        spGameManager.RemoveBall();
        Destroy(gameObject);
    }
}