using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class SpawnGojungAndBack : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject[] StagePrefabs;

    [System.Serializable]
    public class GojungBackData
    {
        public int id;
        public int StagePrefab;
        public string Color;
    }

    void Start()
    {
        int stage = GlobalData.SelectedStage;

        if (jsonFile != null)
        {
            List<GojungBackData> colorDataList = JsonConvert.DeserializeObject<List<GojungBackData>>(jsonFile.text);

            GojungBackData colorData = colorDataList.Find(data => data.id == stage);

            if (colorData != null)
            {
                if (ColorUtility.TryParseHtmlString("#" + colorData.Color, out Color color))
                {
                    GameObject backgroundObject = GameObject.Find("BackGround");

                    if (backgroundObject != null)
                    {
                        SpriteRenderer spriteRenderer = backgroundObject.GetComponent<SpriteRenderer>();

                        if (spriteRenderer != null)
                        {
                            spriteRenderer.color = color;
                        }
                    }

                    if (colorData.StagePrefab > 0 && colorData.StagePrefab <= StagePrefabs.Length)
                    {
                        Instantiate(StagePrefabs[colorData.StagePrefab - 1], new Vector3(0, 0, 0), Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogError("StagePrefab 값이 배열 범위를 벗어납니다.");
                    }
                }
            }
            else
            {
                Debug.LogError($"ID가 {stage}인 데이터를 찾을 수 없습니다.");
            }
        }
    }
}