using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SinglePlayerSetting : MonoBehaviour
{
    public GameObject SinglePlaySetting;
    public GameObject isreallyendless;
    public GameObject neworcontinue;
    public CanvasGroup fadeCanvasGroup; // CanvasGroup 추가

  
    public Button ELYes;
    public Button ELNo;
    public Button Back;
    public Button Back3;
    public Button NewBtn;
    public Button ContinueBtn;
    public Button StoryBtn;
    public Button StoryBtn2;

    public Button ChallengeBtn;
    public Button ChallengeBtn2;

    public Button EndlessBtn;
    public Button EndlessBtn2;

    public Button Back2;


    public GameObject reallynew;
    public Button reallyYes;
    public Button reallyNo;
    void Start()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();
        Back2.onClick.AddListener(() =>
        {
            neworcontinue.SetActive(false);
        });
        StoryBtn.onClick.AddListener(() =>
        {
            neworcontinue.SetActive(true);
        });
        StoryBtn2.onClick.AddListener(() =>
        {
            neworcontinue.SetActive(true);
        });
        Back.onClick.AddListener(() =>
        {
            SinglePlaySetting.SetActive(false);
            neworcontinue.SetActive(false);
        });
        Back3.onClick.AddListener(() =>
        {
            SinglePlaySetting.SetActive(false);
            neworcontinue.SetActive(false);
        });
        NewBtn.onClick.AddListener(() =>
        {
            if (stageGameManager.StageClearID > 1)
            {
                reallynew.SetActive(true);
            }
            else
            {
                ResetStageClearIDAndLoadScene(stageGameManager, "Prologue1.5");
            }
        });
        ContinueBtn.onClick.AddListener(() =>
        {
            if (stageGameManager.StageClearID == 1)
            {
                return;
            }
            else if (stageGameManager.StageClearID <= 6.5f && stageGameManager.StageClearID >= 2)
            {
                if (!stageGameManager.isenglish)
                {
                    StartFadeIn("Stage");
                }
                else
                {
                    StartFadeIn("EStage");
                }
            }
            else if (stageGameManager.StageClearID >= 7)
            {
                if (!stageGameManager.isenglish)
                {
                    StartFadeIn("Main Stage");
                }
                else
                {
                    StartFadeIn("EMain Stage");
                }
            }
        });
        ChallengeBtn.onClick.AddListener(() =>
        {
            StartFadeIn("ChallengeScene");
        });
        ChallengeBtn2.onClick.AddListener(() =>
        {
            StartFadeIn("ChallengeScene");
        });
        EndlessBtn.onClick.AddListener(() =>
        {
            if ((stageGameManager.StageClearID == 0) || (stageGameManager.StageClearID == 1))
            {
                isreallyendless.SetActive(true);
            }
            else
            {
                StartFadeIn("EndlessInGame");
            }
        });
        EndlessBtn2.onClick.AddListener(() =>
        {
            if ((stageGameManager.StageClearID == 0) || (stageGameManager.StageClearID == 1))
            {
                isreallyendless.SetActive(true);
            }
            else
            {
                StartFadeIn("EndlessInGame");
            }
        });
       
        ELYes.onClick.AddListener(() =>
        {
            StartFadeIn("EndlessInGame");
        });
        ELNo.onClick.AddListener(() =>
        {
            isreallyendless.SetActive(false);
        });
        reallyNo.onClick.AddListener(() =>
        {
            reallynew.SetActive(false);
        });
        reallyYes.onClick.AddListener(() =>
        {
            ResetStageClearIDAndLoadScene(stageGameManager, "Prologue1.5");
        });
    }

    void ResetStageClearIDAndLoadScene(StageGameManager stageGameManager, string sceneName)
    {
        stageGameManager.firstTutorialShown = false;
        stageGameManager.secondTutorialShown = false;
        stageGameManager.StageClearID = 1;
        stageGameManager.isending = false;
        PlayerPrefs.SetFloat("StageClearID", stageGameManager.StageClearID);
        PlayerPrefs.SetInt("isending", stageGameManager.isending ? 1 : 0);
        stageGameManager.SaveIsEnding();
        stageGameManager.SaveStageClearID();
        stageGameManager.notfirstTutosave();
        stageGameManager.notsecendtutosave();
        StartFadeIn(sceneName);
    }

    void StartFadeIn(string sceneName)
    {
        fadeCanvasGroup.alpha = 0; // 초기 알파값을 0으로 설정
        fadeCanvasGroup.gameObject.SetActive(true); // CanvasGroup 활성화
        fadeCanvasGroup.DOFade(1, 3f) // 3초 동안 알파값을 1로 애니메이션
            .SetUpdate(true) // TimeScale 영향을 받지 않도록 설정
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneName); // 페이드 인 완료 후 씬 로드
            });
    }
}
