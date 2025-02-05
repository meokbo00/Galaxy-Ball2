using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBulletControl : MonoBehaviour
{
    SPGameManager spGameManager;
    Rigidbody2D rb;
    BGMControl bGMControl;
    float deceleration = 2f;
    public float increase;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private float decelerationThreshold = 0.4f;
    public int PlusScale;
    private float expandSpeed = 1f; // 팽창 속도
    private Vector3 velocity = Vector3.zero;
    private int durability;
    private TextMeshPro textMesh;
    public float fontsize;
    public int BallMinHP = 1;
    public int BallMaxHP = 6;
    public PhysicsMaterial2D bouncyMaterial;
    private Vector3 initialScale; // 초기 공 크기
    private Vector3 targetScale; // 목표 크기
    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string TwiceBulletName = "TwiceBullet(Clone)";
    private const string GojungTag = "Gojung";
    private const string WallTag = "Wall";
    private const string EnemyCenterTag = "EnemyCenter";
    public bool isExpanding = false; // 공이 팽창 중인지 여부



    private void Start()
    {
        spGameManager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        rb = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");

        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        durability = Random.Range(BallMinHP, BallMaxHP);
        textMesh.text = durability.ToString();
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 1;

        rb.drag = 0.1f;
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

        if ((collision.collider.name != SPTwiceFName || collision.collider.name != TwiceBulletName) && rb == null)
        {
            if (collision.collider.CompareTag(GojungTag)) return;
            if (collision.collider.CompareTag(WallTag)) return;
            if(collision.collider.CompareTag(EnemyCenterTag)) return;

            TakeDamage(1);
            textMesh.text = durability.ToString();
        }
        if ((collision.collider.name == SPTwiceFName || collision.collider.name == TwiceBulletName) && rb == null)
        {
            TakeDamage(2);
            textMesh.text = durability.ToString();
        }
    }
    void TakeDamage(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            spGameManager.RemoveBall();
            Destroy(gameObject);
        }
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