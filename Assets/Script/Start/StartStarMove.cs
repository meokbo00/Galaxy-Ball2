using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStarMove : MonoBehaviour
{
    public bool isturn = false;
    public float rotationSpeed = 1f;
    public float radius = 5f;
    public float initialAngle;

    private Vector3 initialPosition;
    private float angle = 0f;

    void Start()
    {
        initialPosition = transform.position; // �ʱ� ��ġ ����
        initialAngle = Random.Range(0f, 360f); // �ʱ� ������ �������� ����
        angle = initialAngle; // �ʱ� ������ ����
    }
    void Update()
    {
        if (isturn)
        {
            // ���� �׸��� �̵�
            angle += rotationSpeed * Time.deltaTime;
            if (angle > 360f) angle -= 360f; // ������ 360���� ���� �ʵ��� ����

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            transform.position = initialPosition + new Vector3(x, y, 0f);
        }
    }
}