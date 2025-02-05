using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SBlackHole_Skill : MonoBehaviour
{
    SPGameManager spgamemanager;
    Rigidbody2D rb;
    BGMControl bGMControl;
    Vector2 lastVelocity;
    float deceleration = 2f;
    public float increase = 4f;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private bool hasBeenReleased = false;
    public PhysicsMaterial2D bouncyMaterial;

    bool hasBeenLaunched = false;
    public bool isExpanding = false; // 공이 팽창 중인지 여부
    private float decelerationThreshold = 0.4f;
    private float dragAmount = 1.1f;
    private float expandSpeed = 1f; // 팽창 속도
    private Vector3 initialScale; // 초기 공 크기
    private Vector3 targetScale; // 목표 크기

    private const string GojungTag = "Gojung";
    private const string WallTag = "Wall";
    private const string EnemyTag = "EnemyCenter";

    private void Start()
    {
        spgamemanager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyObjectDelayed(Random.Range(15f, 25f)));

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
        if (!hasBeenLaunched && !spgamemanager.isDragging)
        {
            LaunchBall();
        }
        if (hasBeenLaunched && !isStopped)
        {
            SlowDownBall();
        }
        if (isExpanding)
        {
            ExpandBall();
        }
    }
    void LaunchBall()
    {
        Debug.Log("공을 발사합니다");
        Vector2 launchForce = SPGameManager.shotDirection * (SPGameManager.shotDistance * 1.4f);
        rb.AddForce(launchForce, ForceMode2D.Impulse);

        rb.drag = dragAmount;
        hasBeenLaunched = true;
    }
    void SlowDownBall()
    {
        if (rb == null) return;

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
        targetScale = initialScale * 10;
        isExpanding = true;
    }
    void ExpandBall()
    {
        if (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            hasExpanded = true;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * expandSpeed);
        }
        else
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
        if ((!collision.collider.CompareTag(GojungTag) || !collision.collider.CompareTag(WallTag) || !collision.collider.CompareTag(EnemyTag) && rb == null))
        {
            spgamemanager.RemoveBall();
            Destroy(collision.gameObject);
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
    IEnumerator DestroyObjectDelayed(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        spgamemanager.RemoveBall();
        Destroy(gameObject);
    }
}