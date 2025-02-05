using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System.Collections.Generic;

public class ShowInformationtxt : MonoBehaviour
{
    public TextAsset JsonFile;
    public TMP_Text informationText;
    public TextMeshProUGUI stageTitleText;

    private Information[] stageInfos;
    StageGameManager gameManager;
    private void Start()
    {
        // JSON 파일에서 데이터 로드
        string jsonString = JsonFile.text;
        stageInfos = JsonConvert.DeserializeObject<Information[]>(jsonString);
        gameManager = FindAnyObjectByType<StageGameManager>();
        // 초기 텍스트 설정
        informationText.text = "";
        stageTitleText.text = "";
    }

    private void Update()
    {
        UpdateStageInfo();
    }

    public void UpdateStageInfo()
    {
        int stageID = StageState.chooseStage;
        float stageClearID = gameManager.StageClearID;

        string stageString = "";
        string stageTitle = "";

        foreach (Information info in stageInfos)
        {
            if (info.id == stageID)
            {
                if (stageClearID < stageID)
                {
                    stageString = "<size=20>???</size>";
                    stageTitle = "<size=12>???</size>";
                }
                else
                {
                    stageString = info.String;
                    stageTitle = info.StageTitle;
                }
                break;
            }
        }

        informationText.text = stageString;
        stageTitleText.text = stageTitle;
    }
}
[System.Serializable]
public class Information
{
    public int id;
    public string StageTitle;
    public string String;
}