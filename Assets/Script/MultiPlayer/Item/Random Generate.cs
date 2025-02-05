using System.Collections;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject[] spherePrefabs;
    public GameObject background;
    public float minSpawnTime = 7f;
    public float maxSpawnTime = 12f;

    private Collider2D backgroundCollider;
    BGMControl bGMControl;

    void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
        backgroundCollider = background.GetComponent<Collider2D>();
        StartCoroutine(SpawnSphereRoutine());
    }

    IEnumerator SpawnSphereRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            SpawnSphere();
        }
    }

    void SpawnSphere()
    {
        Vector2 min = backgroundCollider.bounds.min;
        Vector2 max = backgroundCollider.bounds.max;
        Vector3 randomPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);

        int prefabIndex = Random.Range(0, spherePrefabs.Length);
        Instantiate(spherePrefabs[prefabIndex], randomPosition, Quaternion.identity);
        bGMControl.SoundEffectPlay(2);
    }
}
