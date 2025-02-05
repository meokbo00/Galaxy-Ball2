using UnityEngine;

public class GojungMoving : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.5f; 
    [SerializeField]
    private float distance = 1.7f; 
    private Vector3 targetPosition;
    private bool movingLeft = true;

    void Start()
    {
        // 초기 목표 위치 설정
        targetPosition = transform.position + Vector3.right * distance;
    }

    void Update()
    {
        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 위치에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // 이동 방향 전환
            if (movingLeft)
            {
                targetPosition = transform.position + Vector3.left * distance;
            }
            else
            {
                targetPosition = transform.position + Vector3.right * distance;
            }
            movingLeft = !movingLeft; // 방향 전환
        }
    }
}
