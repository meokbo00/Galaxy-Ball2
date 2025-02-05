using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소를 사용하기 위해 추가
using UnityEngine.EventSystems; // 클릭 이벤트 처리를 위해 추가

public class ShowTutorial : MonoBehaviour
{
    public GameObject[] FirstTutorial;
    public GameObject[] SecondTutorial;
    public GameObject[] EFirstTutorial;
    public GameObject[] ESecondTutorial;
    private int currentIndex = 0; // 현재 활성화된 튜토리얼 이미지의 인덱스
    private List<GameObject> instantiatedImages = new List<GameObject>(); // 생성된 튜토리얼 이미지들을 저장할 리스트
    private StageGameManager stageGameManager;

    void Start()
    {
        stageGameManager = FindObjectOfType<StageGameManager>();

        if (stageGameManager.StageClearID == 1 && !stageGameManager.firstTutorialShown)
        {
            if (!stageGameManager.isenglish)
            {
                ShowTutorialImages(FirstTutorial);
                stageGameManager.firstTutorialShown = true;
                stageGameManager.firstTutosave();
            }
            else
            {
                ShowTutorialImages(EFirstTutorial);
                stageGameManager.firstTutorialShown = true;
                stageGameManager.firstTutosave();
            }
        }
        else if (stageGameManager.StageClearID == 7 && !stageGameManager.secondTutorialShown)
        {
            if (!stageGameManager.isenglish)
            {
                ShowTutorialImages(SecondTutorial);
                stageGameManager.secondTutorialShown = true;
                stageGameManager.secendtutosave();
            }
            else
            {
                ShowTutorialImages(ESecondTutorial);
                stageGameManager.secondTutorialShown = true;
                stageGameManager.secendtutosave();
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void ShowTutorialImages(GameObject[] tutorialArray)
    {
        stageGameManager.PauseGame(); // 게임 일시정지

        foreach (GameObject tutorial in tutorialArray)
        {
            GameObject instantiatedImage = Instantiate(tutorial, new Vector3(540, 960, 0), Quaternion.identity, transform);
            instantiatedImage.SetActive(false); // 일단 비활성화
            instantiatedImages.Add(instantiatedImage);
        }

        if (instantiatedImages.Count > 0)
        {
            instantiatedImages[0].SetActive(true); // 첫 번째 튜토리얼 이미지를 활성화
        }

        // 클릭 이벤트 등록
        foreach (GameObject tutorial in instantiatedImages)
        {
            EventTrigger trigger = tutorial.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { OnImageClick(); });

            trigger.triggers.Add(entry);
        }
    }

    void OnImageClick()
    {
        if (currentIndex < instantiatedImages.Count - 1)
        {
            instantiatedImages[currentIndex].SetActive(false); // 현재 이미지를 비활성화
            currentIndex++;
            instantiatedImages[currentIndex].SetActive(true); // 다음 이미지를 활성화
        }
        else
        {
            // 마지막 이미지인 경우 모든 튜토리얼 이미지를 비활성화하고 게임 재개
            foreach (GameObject tutorial in instantiatedImages)
            {
                tutorial.SetActive(false);
                gameObject.SetActive(false);
            }
            stageGameManager.ResumeGame(); // 게임 재개
        }
    }
}
