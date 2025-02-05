using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStageBox : MonoBehaviour
{
    public TextAsset stageDataFile;
    public TextMeshProUGUI stageTitleText;

    public class Stage
    {
        public int id;
        public string StageTitle;
    }

    private List<Stage> stages;

    void Start()
    {
        stages = JsonConvert.DeserializeObject<List<Stage>>(stageDataFile.text);
    }

    public void UpdateStageInfo(int chooseStage)
    {
        Stage selectedStage = stages.Find(stage => stage.id == chooseStage);
        if (selectedStage != null)
        {
            stageTitleText.text = selectedStage.StageTitle;
        }
        else
        {
            Debug.LogWarning($"Stage {chooseStage} 정보를 찾을 수 없습니다.");
        }
    }
}
