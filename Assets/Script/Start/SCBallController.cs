using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SCBallController : MonoBehaviour
{
    Rigidbody2D rb;
    BGMControl bGMControl;
    bool hasBeenLaunched = false;
    public bool isExpanding = false; // 공이 팽창 중인지 여부
    bool isStopped = false; // 공이 완전히 멈췄는지 여부
    private float decelerationThreshold = 0.4f;
    private float dragAmount = 1.1f;
    private float expandSpeed = 1f; // 팽창 속도
    private Vector3 initialScale; // 초기 공 크기
    private Vector3 targetScale; // 목표 크기
    private int durability; // 공의 내구도
    public PhysicsMaterial2D bouncyMaterial;
    private TextMeshPro textMesh;

    public int fontsize;
    public int PlusScale;


    void Start()
    {
        bGMControl = FindAnyObjectByType<BGMControl>();
        rb = GetComponent<Rigidbody2D>();

        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform; // 구체의 자식으로 설정
        durability = Random.Range(1, 6);

        textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = durability.ToString();
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero; // 구체 중심에 텍스트 배치
        textMesh.sortingOrder = 1; // 레이어 순서를 조정하여 구체 위에 배치

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

    void Update()
    {
        if (!hasBeenLaunched)
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
        Vector2 launchForce = SCGameManager.shotDirection * (SCGameManager.shotDistance * 1.4f);
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
        bGMControl.SoundEffectPlay(1);
        targetScale = initialScale * PlusScale;
        isExpanding = true;
    }

    void ExpandBall()
    {
        if (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
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
        if (!isExpanding)
        {
            bGMControl.SoundEffectPlay(0);
        }
        if (!collision.collider.isTrigger && isExpanding)
        {
            isExpanding = false; // 팽창 중단
            transform.localScale = transform.localScale; // 현재 크기에서 멈춤
            DestroyRigidbody(); // Rigidbody 제거
        }

        if ((collision.collider.tag == "P1ball" || collision.collider.tag == "P2ball") && rb == null)
        {
            TakeDamage(1);
            textMesh.text = durability.ToString();
        }
    }

    void TakeDamage(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
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
