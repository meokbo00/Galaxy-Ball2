using UnityEngine;
using UnityEngine.SceneManagement;

public class SPFailSceneManager : MonoBehaviour
{
    private StageGameManager stageGameManager;

    void Start()
    {
        // Start에서 StageGameManager를 한 번만 찾도록 변경
        stageGameManager = FindObjectOfType<StageGameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 위치를 기준으로 RaycastHit2D 실행
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null) return;

            // 히트된 오브젝트의 이름에 따라 처리
            string hitName = hit.collider.gameObject.name;

            if (hitName == "GoStage")
            {
                LoadStage();
            }
            else if (hitName == "GoMenu")
            {
                SceneManager.LoadScene("Start Scene");
            }
            else if (hitName == "Retry")
            {
                SceneManager.LoadScene("Story-InGame");
            }
            else if (hitName == "Random")
            {
                LoadRandomScene();
            }
        }
    }

    // 스테이지를 로드하는 함수
    private void LoadStage()
    {
        if (stageGameManager != null)
        {
            if (stageGameManager.StageClearID <= 5)
            {
                SceneManager.LoadScene("Stage");
            }
            else if (stageGameManager.StageClearID >= 6)
            {
                SceneManager.LoadScene("Main Stage");
            }
        }
        else
        {
            Debug.LogWarning("StageGameManager를 찾을 수 없습니다.");
        }
    }

    // 랜덤한 씬을 로드하는 함수
    private void LoadRandomScene()
    {
        int randomnumber = Random.Range(1, 6);

        switch (randomnumber)
        {
            case 1:
                SceneManager.LoadScene("Example Scene");
                break;
            case 2:
                SceneManager.LoadScene("Design Scene");
                break;
            case 3:
                SceneManager.LoadScene("1-1 Intro Scene");
                break;
            case 4:
                SceneManager.LoadScene("Credit Scene");
                break;
            case 5:
                SceneManager.LoadScene("ChallengeScene");
                break;
        }
    }
}
