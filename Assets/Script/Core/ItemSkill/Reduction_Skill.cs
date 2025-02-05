using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reduction_Skill : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 lastVelocity;
    BGMControl bGMControl;
    float deceleration = 2f;
    public float increase = 4f;
    private bool iscolliding = false;
    public bool hasExpanded = false;
    private bool isStopped = false;
    private bool hasBeenReleased = false;

    private void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && rigid != null && !hasBeenReleased)
        {
            rigid.velocity = GameManager.shotDirection * GameManager.shotDistance;
            hasBeenReleased = true;
        }
        Move();
        expand();
    }

    void Move()
    {
        if (rigid == null || isStopped) return;

        lastVelocity = rigid.velocity;
        rigid.velocity -= rigid.velocity.normalized * deceleration * Time.deltaTime;

        if (rigid.velocity.magnitude <= 0.01f && hasExpanded)
        {
            isStopped = true;
            StartCoroutine(DestroyRigidbodyDelayed());
        }
    }
    void expand()
    {
        if (rigid == null || iscolliding) return;
        if (rigid.velocity.magnitude > 0.01f) return;
        if (Input.GetMouseButton(0)) return;

        if (!hasExpanded)
        {
            bGMControl.SoundEffectPlay(1);
        }
        transform.localScale += Vector3.one * increase * Time.deltaTime;
        hasExpanded = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!hasExpanded)
        {
            bGMControl.SoundEffectPlay(0);
        }
        if ((coll.gameObject.tag == "P1ball" || coll.gameObject.tag == "P2ball" || coll.gameObject.tag == "P1Item" || coll.gameObject.tag == "P2Item"
            || coll.gameObject.tag == "EnemyBall" || coll.gameObject.tag == "Item") && rigid == null)
        {
            Shrink();
        }
        if (coll.contacts != null && coll.contacts.Length > 0)
        {
            Vector2 dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
            if (rigid != null)
                rigid.velocity = dir * Mathf.Max(lastVelocity.magnitude, 0f);
        }
        this.iscolliding = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        this.iscolliding = false;
    }
    private void Shrink()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x -= 0.5f;
        currentScale.y -= 0.5f;
        currentScale.x = Mathf.Max(currentScale.x, 0.4f);
        currentScale.y = Mathf.Max(currentScale.y, 0.4f);
        transform.localScale = currentScale;

        if (currentScale.x <= 0.4f && currentScale.y <= 0.4f)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator DestroyRigidbodyDelayed()
    {
        yield return new WaitForSeconds(0.8f);
        if (rigid != null)
            Destroy(rigid);
    }
}