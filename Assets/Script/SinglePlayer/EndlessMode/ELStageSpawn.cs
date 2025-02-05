using UnityEngine;
using System.Collections.Generic; // List를 사용하기 위해 추가

public class ELStageSpawn : MonoBehaviour
{
    public GameObject[] EnemyPrefabs; // 생성할 적 프리팹 배열
    public GameObject[] GojungPrefabs;
    public GameObject BackGround;
    public static StageGameManager stageGameManager;

    private void Start()
    {
        stageGameManager = FindAnyObjectByType<StageGameManager>();
        if (stageGameManager != null)
        {
            SpawnEnemies(stageGameManager.ELlevel);
        }
        SpawnRandomGojungPrefab();
        RandomizeBackgroundColor();
    }
    private void RandomizeBackgroundColor()
    {
        if (BackGround != null)
        {
            SpriteRenderer spriteRenderer = BackGround.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // RGB 값을 랜덤으로 설정 (0.0f ~ 1.0f 범위)
                float r = Random.Range(0f, 1f);
                float g = Random.Range(0f, 1f);
                float b = Random.Range(0f, 1f);
                spriteRenderer.color = new Color(r, g, b);
                Debug.Log($"Background color randomized to: R={r}, G={g}, B={b}");
            }
        }
    }
    private void SpawnRandomGojungPrefab()
    {
        if (GojungPrefabs.Length > 0)
        {
            GameObject selectedGojung = GojungPrefabs[Random.Range(0, GojungPrefabs.Length)];
            Instantiate(selectedGojung, Vector3.zero, Quaternion.identity);
        }
    }
    public void SpawnEnemies(float targetLevel)
    {
        // ELlevel보다 작은 EnemyStat을 가진 프리팹 필터링
        List<GameObject> validPrefabs = new List<GameObject>();

        foreach (var prefab in EnemyPrefabs)
        {
            ELEnemyStat stat = prefab.GetComponent<ELEnemyStat>();
            if (stat != null)
            {
                if (stat.EnemyStat <= targetLevel)
                {
                    validPrefabs.Add(prefab);
                }
            }
        }

        foreach (var validPrefab in validPrefabs)
        {
            ELEnemyStat stat = validPrefab.GetComponent<ELEnemyStat>();
        }

        // 유효한 프리팹이 없는 경우
        if (validPrefabs.Count == 0)
        {
            Debug.LogWarning("ELlevel보다 작은 EnemyStat을 가진 프리팹이 없습니다.");
            return;
        }

        float currentTotal = 0f;
        int safetyCounter = 15; // 안전 장치
        int spawnCount = 0;

        // 목표와 오차 범위 내인지 확인하며 생성
        while (Mathf.Abs(currentTotal - targetLevel) >= 0.1f && safetyCounter > 0)
        {
            safetyCounter--;

            // 유효한 프리팹 중 하나를 랜덤으로 선택
            GameObject selectedEnemy = validPrefabs[Random.Range(0, validPrefabs.Count)];
            ELEnemyStat stat = selectedEnemy.GetComponent<ELEnemyStat>();

            Debug.Log($"Loop Iteration: CurrentTotal = {currentTotal}, TargetLevel = {targetLevel}, SelectedPrefabStat = {stat.EnemyStat}");

            if (currentTotal + stat.EnemyStat > targetLevel)
            {
                // 초과 허용 범위 내인지 확인 (최대 1.0f 초과)
                if ((currentTotal + stat.EnemyStat) - targetLevel <= 1.0f) // 초과 허용 범위
                {
                    currentTotal += stat.EnemyStat;
                    Debug.Log($"Selected prefab {selectedEnemy.name} to slightly exceed target. Final Total: {currentTotal}");
                    break; // 목표값에 도달하여 루프 종료
                }

                continue; // 목표값을 넘지 않도록 다음 프리팹으로 넘어감
            }

            // 적 생성
            Vector2 spawnPosition = new Vector2(
                Random.Range(-2.4f, 2.4f),
                Random.Range(-2.7f, 4.5f)
            );
            Instantiate(selectedEnemy, spawnPosition, Quaternion.identity);

            // 현재 합계 업데이트
            currentTotal += stat.EnemyStat;
            spawnCount++;

            Debug.Log($"Spawned: {spawnCount}, CurrentTotal: {currentTotal}, Target: {targetLevel}");
        }

        if (safetyCounter == 0)
        {
            Debug.LogWarning($"SpawnEnemies: 안전 장치 발동! 목표 달성 실패. CurrentTotal: {currentTotal}, Target: {targetLevel}");
        }

        Debug.Log($"Total Spawned: {spawnCount}, Final Total: {currentTotal}");
    }
}
