using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�
using UnityEngine.UI;

public class Ch3Story : MonoBehaviour
{
    public Camera Camera;
    public GameObject Fadein;
    public AudioSource[] AudioSources; // ����� �ҽ� �迭
    StageGameManager stageGameManager;
    TextManager textManager;
    ShowText showText;
    public Button transitionButton; // TMP 버튼은 일반 Button과 함께 사용됩니다.
    private Coroutine deactivateButtonCoroutine;

    private bool isZooming = false;

    private bool isAudio1Played = false; // ù ��° ����� ��� ����
    private bool isAudio2Played = false; // �� ��° ����� ��� ����

    void Start()
    {
        showText = FindObjectOfType<ShowText>();
        textManager = FindObjectOfType<TextManager>();
        stageGameManager = FindObjectOfType<StageGameManager>();
        transitionButton.gameObject.SetActive(false); // 시작할 때 버튼 비활성화
        transitionButton.onClick.AddListener(OnButtonClick); // 버튼 클릭 시 이벤트 추가
        if (stageGameManager.StageClearID == 65)
        {
            textManager.GiveMeTextId(1);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            ActivateButton();
        }
        if (stageGameManager.StageClearID == 65)
        {
            showText = FindObjectOfType<ShowText>();

            if (showText.logTextIndex == 8 && !isZooming)
            {
                StartCoroutine(SmoothZoom(5f, 1700f));
            }
            if (showText.logTextIndex == 12)
            {
                StartCoroutine(ExecuteAfterDelay(4f));
            }
            if (showText.logTextIndex == 31)
            {
                StartCoroutine(LoadSceneAfterDelay(4f, "Story-InGame"));
            }
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
    void OnButtonClick()
    {
        SceneManager.LoadScene("Story-InGame");
    }
    IEnumerator DeactivateButtonAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (transitionButton.gameObject.activeSelf)
        {
            transitionButton.gameObject.SetActive(false); // 버튼 비활성화
        }
    }
    IEnumerator SmoothZoom(float duration, float targetSize)
    {
        isZooming = true;
        float startSize = Camera.orthographicSize;
        float elapsed = 0f;

        if (AudioSources.Length > 0 && !isAudio1Played)
        {
            AudioSources[0].Play();
            isAudio1Played = true; // ù ��° ����� ��� ���� ����
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Camera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            yield return null;
        }

        Camera.orthographicSize = targetSize;
        isZooming = false;
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fadein.SetActive(true);

        // ����� �ҽ� �迭�� �� ��° ������� ������� ���� ��� ���
        if (AudioSources.Length > 1 && !isAudio2Played)
        {
            AudioSources[1].Play();
            isAudio2Played = true; // �� ��° ����� ��� ���� ����
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
