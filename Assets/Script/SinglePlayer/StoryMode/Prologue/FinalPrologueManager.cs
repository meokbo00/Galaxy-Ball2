using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalPrologueManager : MonoBehaviour
{
    public GameObject[] images;
    public GameObject[] Eimages;
    public Button transitionButton;

    private Coroutine deactivateButtonCoroutine;
    private CanvasGroup[] canvasGroups;
    StageGameManager stageGameManager;

    void Start()
    {
        stageGameManager = FindAnyObjectByType<StageGameManager>();
        // 언어에 맞는 이미지를 선택하기 위해 currentImages 배열을 설정합니다.
        GameObject[] currentImages = stageGameManager.isenglish ? Eimages : images;

        canvasGroups = new CanvasGroup[currentImages.Length];

        // 선택한 배열에 대해 CanvasGroup을 초기화합니다.
        for (int i = 0; i < currentImages.Length; i++)
        {
            canvasGroups[i] = currentImages[i].GetComponent<CanvasGroup>() ?? currentImages[i].AddComponent<CanvasGroup>();
            canvasGroups[i].alpha = 0f;
        }

        transitionButton.gameObject.SetActive(false); // 시작 시 버튼 비활성화
        transitionButton.onClick.AddListener(OnButtonClick); // 버튼 클릭 이벤트 추가
        StartCoroutine(ShowImagesSequentially());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            ActivateButton();
        }
    }

    IEnumerator ShowImagesSequentially()
    {
        float waitTime = 3.7f;

        // 현재 언어에 맞는 이미지 배열을 선택합니다.
        GameObject[] currentImages = stageGameManager.isenglish ? Eimages : images;

        foreach (GameObject image in currentImages)
        {
            yield return StartCoroutine(Fade(image, true, 2f)); // 페이드 인
            yield return new WaitForSeconds(waitTime); // 대기 시간 설정
            yield return StartCoroutine(Fade(image, false, 1f)); // 페이드 아웃
        }

        yield return new WaitForSeconds(2f);
        // 언어에 따라 다른 씬을 로드합니다.
        if (!stageGameManager.isenglish)
        {
            SceneManager.LoadScene("Stage");
        }
        else
        {
            SceneManager.LoadScene("EStage");
        }
    }

    IEnumerator Fade(GameObject image, bool fadeIn, float duration)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        image.SetActive(true);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(fadeIn ? (elapsed / duration) : (1f - (elapsed / duration)));
            yield return null;
        }

        canvasGroup.alpha = fadeIn ? 1f : 0f;

        if (!fadeIn)
        {
            image.SetActive(false); // 페이드 아웃 후 비활성화
        }
    }

    void ActivateButton()
    {
        if (!transitionButton.gameObject.activeSelf) // 이미 활성화되어 있는지 확인
        {
            transitionButton.gameObject.SetActive(true); // 버튼 활성화
            if (deactivateButtonCoroutine != null)
            {
                StopCoroutine(deactivateButtonCoroutine);
            }
            deactivateButtonCoroutine = StartCoroutine(DeactivateButtonAfterDelay());
        }
    }

    IEnumerator DeactivateButtonAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (transitionButton.gameObject.activeSelf)
        {
            transitionButton.gameObject.SetActive(false); // 버튼 비활성화
        }
    }

    void OnButtonClick()
    {
        // 버튼 클릭 시 언어에 맞는 씬 로드
        if (!stageGameManager.isenglish)
        {
            SceneManager.LoadScene("Stage");
        }
        else
        {
            SceneManager.LoadScene("EStage");
        }
    }
}
