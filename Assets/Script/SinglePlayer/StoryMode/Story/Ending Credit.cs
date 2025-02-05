using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingCredit : MonoBehaviour
{
    StageGameManager stageGameManager;
    public GameObject endingcredit;
    public Button transitionButton; // TMP 버튼은 일반 Button과 함께 사용됩니다.
    private Coroutine deactivateButtonCoroutine;

    public float scrollSpeed = 20f;

    private void Start()
    {
        stageGameManager = FindObjectOfType<StageGameManager>();
        transitionButton.gameObject.SetActive(false); // 시작할 때 버튼 비활성화
        transitionButton.onClick.AddListener(OnButtonClick); // 버튼 클릭 시 이벤트 추가
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            ActivateButton();
        }
        endingcredit.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (endingcredit.transform.position.y >= 14500f)
        {
            stageGameManager.isending = true;
            stageGameManager.SaveIsEnding(); // isending 값을 저장
            SceneManager.LoadScene("Start Scene");
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
        stageGameManager.isending = true;
        stageGameManager.SaveIsEnding(); // isending 값을 저장
        SceneManager.LoadScene("Start Scene");
    }
}
