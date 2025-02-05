using System.Collections;
using UnityEngine;

public class SCGameManager : MonoBehaviour
{
    public GameObject P1ballPrefab;
    public GameObject P2ballPrefab;
    public GameObject P1firezone;
    public GameObject P2firezone;
    public GameObject ballPrefab;


    private bool P1FireMode = true;
    private bool P2FireMode = false;

    public static float shotDistance;
    public static Vector3 shotDirection;

    private void Start()
    {
        P1firezone.gameObject.SetActive(true);
        P2firezone.gameObject.SetActive(true);
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2.5f, 3f));

            GameObject fireZone;

            if (P1FireMode)
            {
                ballPrefab = P1ballPrefab;
                fireZone = P1firezone;
            }
            else
            {
                ballPrefab = P2ballPrefab;
                fireZone = P2firezone;
            }

            Vector3 spawnPosition = GetRandomPositionInFireZone(fireZone);
            GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

            CalculateRandomShot();

            SwitchFireMode();
        }
    }

    private Vector3 GetRandomPositionInFireZone(GameObject fireZone)
    {
        Collider2D zoneCollider = fireZone.GetComponent<Collider2D>();
        Bounds bounds = zoneCollider.bounds;
        Vector3 randomPoint = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
        return randomPoint;
    }

    private void SwitchFireMode()
    {
        P1FireMode = !P1FireMode;
        P2FireMode = !P2FireMode;
        P1firezone.gameObject.SetActive(P1FireMode);
        P2firezone.gameObject.SetActive(P2FireMode);
    }

    private void CalculateRandomShot()
    {
        shotDistance = Random.Range(7f, 13f); // ���� ��, ���ϴ� ������ ���� ����
        float x = Random.Range(0f, 360f); // 0���� 360�� ������ ����
        float y = Random.Range(0f, 360f);
        shotDirection = new Vector3(x, y, 0).normalized; // ������ ���ͷ� ��ȯ
    }
}