using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class EnemySpawnData
{
    public string type;
    public float x;
    public float y;
}

public class EnemyData
{
    public int id;
    public List<EnemySpawnData> enemies;
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] Enemy;
    public TextAsset jsonFile;

    void Start()
    {
        int stage = GlobalData.SelectedStage;
        string jsonData = jsonFile.text; // JSON 데이터를 TextAsset에서 가져옵니다.
        List<EnemyData> enemyDataList = JsonConvert.DeserializeObject<List<EnemyData>>(jsonData);

        EnemyData data = enemyDataList.Find(d => d.id == stage);
        if (data != null)
        {
            foreach (var enemy in data.enemies)
            {
                if (!string.IsNullOrEmpty(enemy.type))
                {
                    InstantiateEnemy(enemy.type, enemy.x, enemy.y);
                }
            }
        }
    }

    void InstantiateEnemy(string enemyType, float x, float y)
    {
        int index = GetEnemyIndex(enemyType);
        if (index >= 0 && index < Enemy.Length)
        {
            // Instantiate the enemy and set its position correctly
            GameObject newEnemy = Instantiate(Enemy[index], new Vector3(x, y, 0), Quaternion.identity);
            Debug.Log($"Instantiated {enemyType} at position ({x}, {y})");
        }
    }

    int GetEnemyIndex(string enemyType)
    {
        switch (enemyType)
        {
            case "Enemy1": return 0;
            case "Enemy2": return 1;
            case "Enemy3": return 2;
            case "Enemy4": return 3;
            case "Enemy5": return 4;
            case "Enemy6": return 5;
            case "HEnemy1": return 6;
            case "HEnemy2": return 7;
            case "SEnemy1": return 8;
            case "SEnemy2": return 9;
            case "SEnemy3": return 10;
            case "TEnemy1": return 11;
            case "TEnemy2": return 12;
            case "XEnemy1": return 13;
            case "XEnemy2": return 14;
            case "XEnemy3": return 15;
            case "YEnemy2": return 16;
            case "ZEnemy1": return 17;
            case "ZEnemy2": return 18;
            case "ZEnemy3": return 19;
            case "ZEnemy4": return 20;
            case "ZEnemy5": return 21;
            case "ZEnemy6": return 22;
            case "ZEnemy7": return 23;
            case "IEnemy1": return 24;
            case "IEnemy2": return 25;
            case "IEnemy3": return 26;
            case "IEnemy4": return 27;
            case "IEnemy5": return 28;


            default: return -1;
        }
    }
}