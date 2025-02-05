using UnityEngine;

public class SPRandomGenerate : MonoBehaviour
{
    private BGMControl bGMControl;
    public GameObject[] spherePrefabs;
    public GameObject background;
    public float minSpawnTime = 7f;
    public float maxSpawnTime = 12f;

    private Collider2D backgroundCollider;
    private StageGameManager stageGameManager;
    private int maxIndex = 0;

    void Start()
    {
        // 필요한 오브젝트 캐싱
        bGMControl = FindObjectOfType<BGMControl>();
        stageGameManager = FindObjectOfType<StageGameManager>();
        backgroundCollider = background.GetComponent<Collider2D>();

        SetMaxIndex();

        // 지정된 간격으로 SpawnSphere를 호출
        float initialSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        InvokeRepeating("SpawnSphere", initialSpawnTime, initialSpawnTime);
    }

    // StageClearID에 따른 maxIndex 설정
    private void SetMaxIndex()
    {
        float stageID = stageGameManager.StageClearID;

        if (stageID <= 6)
        {
            maxIndex = -1;
        }
        else if (stageID <= 10)
        {
            maxIndex = 0;
        }
        else if (stageID <= 17)
        {
            maxIndex = 1;
        }
        else if (stageID <= 21)
        {
            maxIndex = 2;
        }
        else if (stageID <= 34)
        {
            maxIndex = 3;
        }
        else if (stageID <= 44)
        {
            maxIndex = 4;
        }
        else if (stageID == 45 || stageID == 64 || stageID == 65)
        {
            maxIndex = 3;
        }
        else
        {
            maxIndex = 4;
        }
    }

    // 구체 생성
    private void SpawnSphere()
    {
        if (maxIndex < 0) return; // 스폰 조건을 충족하지 않으면 종료

        // 배경 내의 무작위 위치 계산
        Vector2 min = backgroundCollider.bounds.min;
        Vector2 max = backgroundCollider.bounds.max;
        Vector3 randomPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0f);

        // 구체 프리팹 중 하나를 무작위로 선택하여 인스턴스화
        int prefabIndex = Random.Range(0, maxIndex + 1);
        if (bGMControl.SoundEffectSwitch)
        {
            bGMControl.SoundEffectPlay(2);
        }
        Instantiate(spherePrefabs[prefabIndex], randomPosition, Quaternion.identity);

        // 다음 스폰 간격을 랜덤 설정
        float nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        CancelInvoke("SpawnSphere");
        InvokeRepeating("SpawnSphere", nextSpawnTime, nextSpawnTime);
        Debug.Log("MaxIndex : " + maxIndex);
    }
}
