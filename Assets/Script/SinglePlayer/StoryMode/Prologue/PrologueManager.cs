using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening; // DOTween 네임스페이스 추가

public class PrologueManager : MonoBehaviour
{
    public Image firstImage;
    public Image secondImage;
    public Image fadeImage;

    private TextManager textManager;
    private ShowText showTextScript;
    private int currentChatId;

    void Start()
    {
        // TextManager와 ShowText 스크립트를 한 번만 찾고 캐시
        textManager = FindObjectOfType<TextManager>();
        if (textManager != null)
        {
            showTextScript = textManager.Linebox.GetComponent<ShowText>();
        }

        // 첫 번째 이미지 페이드 인/아웃 시작
        FadeInAndOutImage(firstImage);
    }

    void FadeInAndOutImage(Image image)
    {
        // 페이드 인 (2초)
        image.DOFade(1, 2f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                // 1초 대기 후 페이드 아웃
                DOVirtual.DelayedCall(1f, () =>
                {
                    image.DOFade(0, 2f)
                        .SetUpdate(true)
                        .OnComplete(() =>
                        {
                            image.gameObject.SetActive(false);
                            secondImage.gameObject.SetActive(true);

                            // 4.5초 대기 후 오디오 및 텍스트 처리 시작
                            DOVirtual.DelayedCall(4.5f, () =>
                            {
                                AudioSource audioSource = secondImage.GetComponent<AudioSource>();
                                if (audioSource != null && showTextScript != null)
                                {
                                    audioSource.Stop();

                                    // OnChatComplete 이벤트 리스너 등록
                                    showTextScript.OnChatComplete.AddListener(OnChatComplete);
                                    currentChatId = 1;
                                    textManager.GiveMeTextId(currentChatId);
                                }
                            }).SetUpdate(true);
                        });
                }).SetUpdate(true);
            });
    }

    void OnChatComplete(int chatId)
    {
        Debug.Log($"아이디 {chatId}에 해당하는 문장을 출력완료했습니다.");
        if (chatId == 1)
        {
            FadeInImage(fadeImage);

            // 이벤트 리스너 해제하여 메모리 관리 및 최적화
            showTextScript.OnChatComplete.RemoveListener(OnChatComplete);
        }
    }

    void FadeInImage(Image image)
    {
        image.gameObject.SetActive(true);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        // 페이드 인 (3초)
        image.DOFade(1, 3f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                // 2.5초 대기 후 다음 씬으로 전환
                DOVirtual.DelayedCall(2.5f, () =>
                {
                    SceneManager.LoadScene("Prologue1.5");
                }).SetUpdate(true);
            });
    }
}
