using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ch1Story : MonoBehaviour
{
    public GameObject Clearhere;
    public Camera mainCamera;
    public Image FadeIn;
    public GameObject Stage;
    public GameObject RemainTime;

    public GameObject story1_2;

    private StageGameManager stageGameManager;
    private ShowText showText;
    private StageBallController stageBallController;
    private ContinuousRandomMovement[] randomMovements;
    BGMControl bGMControl;
    void Start()
    {
        // 필요한 컴포넌트 미리 캐싱
        bGMControl = FindAnyObjectByType<BGMControl>();
        stageGameManager = FindObjectOfType<StageGameManager>();
        TextManager textManager = FindObjectOfType<TextManager>();
        stageBallController = FindObjectOfType<StageBallController>();
        showText = FindObjectOfType<ShowText>();
        randomMovements = FindObjectsOfType<ContinuousRandomMovement>();
        if(stageGameManager.StageClearID <6)
        {
            story1_2.gameObject.SetActive(false);
        }
        else {story1_2.gameObject.SetActive(true);}

        switch (stageGameManager.StageClearID)
        {
            case 1:
                stageBallController.enabled = false;
                textManager.GiveMeTextId(1);
                break;
            case 2:
                textManager.GiveMeTextId(2);
                break;
            case 5:
                Destroy(Stage);
                textManager.GiveMeTextId(3);
                break;
            case 6:
                Destroy(Stage);
                RemainTime.SetActive(true);
                break;
        }
    }

    void FixedUpdate()
    {
        if (showText == null) // 한 번만 탐색
        {
            showText = FindAnyObjectByType<ShowText>();
            if (showText == null) return;
        }

        if (stageGameManager.StageClearID == 1 )
        {
            HandleStage1();
        }

        if (stageGameManager.StageClearID == 5 )
        {
            HandleStage5();
        }
    }


    private void HandleStage1() // 1번 연출
    {
        if (showText.logTextIndex == 11)
        {
            stageBallController.enabled = true; // 플레이어 이동 허용
        }
        else if(showText.logTextIndex == 19)
        {
            stageBallController.enabled = false; // 플레이어 이동 불허
        }
        else if (showText.logTextIndex == 26)
        {
            Clearhere.SetActive(true);  // 클리어로고 활성화
            Stage.SetActive(true);  // 스테이지 활성화
        }
        else if(showText.logTextIndex == 30)
        {
            stageBallController.enabled = true; // 플레이어 이동 허용
        }
        if (showText.logTextIndex < 25)
        {
            Stage.SetActive(false); // 스테이지 비활성화
        }
    }

    private void HandleStage5() // 5번 연출
    {
        if (showText.logTextIndex == 4)
        {
            StartCoroutine(IncreaseCameraSize(mainCamera, 112, 5)); // 카메라 축소
        }

        if (showText.logTextIndex == 8)
        {
            ToggleRandomMovement(0); // 다른 구체들 정지
        }

        if (showText.logTextIndex == 24)
        {
            ToggleRandomMovement(5); // 다른 구체들 움직임
            StartCoroutine(HandleCameraAndFadeIn(mainCamera, 15, 7f)); // 카메라 확대 + 시간 보여주고 씬변환
        }
    }

    private void ToggleRandomMovement(int speed)
    {
        foreach (var randomMovement in randomMovements)
        {
            randomMovement.moveSpeed = speed;
        }
    }

    private IEnumerator IncreaseCameraSize(Camera camera, float targetSize, float duration)
    {
        float startSize = camera.orthographicSize;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            camera.orthographicSize = Mathf.Lerp(startSize, targetSize, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        camera.orthographicSize = targetSize;
    }

    private IEnumerator HandleCameraAndFadeIn(Camera camera, float targetSize, float duration)
    {
        // 카메라 확대
        yield return StartCoroutine(IncreaseCameraSize(camera, targetSize, duration));

        RemainTime.SetActive(true);
        yield return new WaitForSeconds(15f);

        FadeIn.gameObject.SetActive(true);
        if(bGMControl.SoundEffectSwitch)
        {
            bGMControl.SoundEffectPlay(5);
        }
        yield return new WaitForSeconds(4.5f);

        stageGameManager.StageClearID += 1;
        stageGameManager.SaveStageClearID();
        SceneManager.LoadScene("Prologue 2");
    }

}
