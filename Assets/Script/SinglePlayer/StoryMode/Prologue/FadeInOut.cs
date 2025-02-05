using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public Image[] images;
    public float fadeTime = 1f;

    void Start()
    {
        // 모든 이미지를 비활성화
        foreach (var image in images)
        {
            image.gameObject.SetActive(false);
        }

        StartCoroutine(SequentialFadeEffect());
    }

    IEnumerator SequentialFadeEffect()
    {
        for (int i = 0; i < images.Length; i++)
        {
            yield return StartCoroutine(FadeEffect(images[i]));
            images[i].gameObject.SetActive(false);
        }

        SceneManager.LoadScene("Start Scene");
    }

    IEnumerator FadeEffect(Image image)
    {
        image.gameObject.SetActive(true);

        // 페이드 인
        for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime / fadeTime)
        {
            Color color = image.color;
            color.a = t;  // 직접 a 값 설정
            image.color = color;
            yield return null;  // 다음 프레임까지 대기
        }
        // 완전히 보이도록 설정
        Color fullVisibleColor = image.color;
        fullVisibleColor.a = 1f;
        image.color = fullVisibleColor;

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 페이드 아웃
        for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime / fadeTime)
        {
            Color color = image.color;
            color.a = 1f - t;  // 직접 a 값 설정
            image.color = color;
            yield return null;  // 다음 프레임까지 대기
        }
        // 완전히 보이지 않도록 설정
        Color invisibleColor = image.color;
        invisibleColor.a = 0f;
        image.color = invisibleColor;
    }
}
