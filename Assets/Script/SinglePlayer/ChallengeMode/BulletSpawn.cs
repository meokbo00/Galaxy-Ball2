using UnityEngine;
using System.Collections.Generic;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnInterval = 10f;
    public float minimumSpawnInterval = 1f;
    public float minForce = 1.7f;
    public float maxForce = 7f;

    private BoxCollider2D backgroundCollider;
    private float timer;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    void Start()
    {
        GameObject background = GameObject.Find("BackGround");
        if (background != null)
        {
            backgroundCollider = background.GetComponent<BoxCollider2D>();
        }

        // 오브젝트 풀 초기화
        for (int i = 0; i < 50; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }

        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnBullet();
            spawnInterval = Mathf.Max(minimumSpawnInterval, spawnInterval - 0.2f);
            timer = spawnInterval;
        }
    }

    void SpawnBullet()
    {
        if (bulletPool.Count == 0) return;

        GameObject bullet = bulletPool.Dequeue();
        bullet.SetActive(true);

        float x = Random.Range(backgroundCollider.bounds.min.x, backgroundCollider.bounds.max.x);
        float y = Random.Range(backgroundCollider.bounds.min.y, backgroundCollider.bounds.max.y);
        bullet.transform.position = new Vector2(x, y);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 forceDirection = Random.insideUnitCircle.normalized;
            float forceMagnitude = Random.Range(minForce, maxForce);
            rb.velocity = Vector2.zero; // 이전 힘 초기화
            rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
        }

        // 풀에 총알 반환하기 위한 메서드 구현 필요
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
