using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy6LegControl : MonoBehaviour
{
    public float moveSpeed = 1f;  // 이동 속도
    public float rotationSpeed = 180f;  // 회전 속도
    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string SPEndlessFName = "SPEndlessF(Clone)";
    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent;  // 부모 Transform 저장
        StartCoroutine(MoveAndRotate());
    }

    private IEnumerator MoveAndRotate()
    {
        while (true)
        {
            // 회전
            float targetAngle = Random.Range(-70f, 70f);
            float currentAngle = parentTransform.eulerAngles.z;

            // 현재 각도와 목표 각도가 다를 때만 회전 시작
            if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.01f)
            {
                float rotationTime = Mathf.Abs(targetAngle - currentAngle) / rotationSpeed;
                float rotationElapsedTime = 0f; // 변경된 부분

                while (rotationElapsedTime < rotationTime)
                {
                    rotationElapsedTime += Time.deltaTime;
                    float angle = Mathf.LerpAngle(currentAngle, targetAngle, rotationElapsedTime / rotationTime);
                    parentTransform.eulerAngles = new Vector3(0, 0, angle);
                    yield return null;  // 매 프레임 대기
                }
            }

            // 이동 방향 결정
            Vector3 moveDirection = Random.Range(0, 2) == 0 ? -parentTransform.up : parentTransform.up;

            float moveTime = Random.Range(1.5f, 2.5f);
            float moveElapsedTime = 0f; // 변경된 부분

            while (moveElapsedTime < moveTime)
            {
                moveElapsedTime += Time.deltaTime;
                parentTransform.position += moveDirection * moveSpeed * Time.deltaTime;
                yield return null;  // 매 프레임 대기
            }

            // 대기 시간 추가
            float waitTime = Random.Range(2f, 3f);
            yield return new WaitForSeconds(waitTime);  // 대기 후 다음 루프 실행
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy6center enemy6Center = GetComponent<Enemy6center>();

        if (collision.gameObject.tag == "EnemyBall") return;
        if (collision.gameObject.tag == "Gojung") return;
        if (collision.gameObject.tag == "Wall") return;

        if (collision.gameObject.name != SPEndlessFName)
        {
            enemy6Center.TakeDamage(1);
        }
        if (collision.gameObject.name == SPTwiceFName)
        {
            enemy6Center.TakeDamage(1);
        }
    }
}
