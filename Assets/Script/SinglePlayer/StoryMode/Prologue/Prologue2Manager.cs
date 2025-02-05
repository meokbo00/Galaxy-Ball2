using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue2Manager : MonoBehaviour
{
    public Camera Camera;
    public GameObject Star;
    public GameObject[] Sun;
    public GameObject white;

    void Start()
    {
        StartCoroutine(AdjustCameraSize());
        StartCoroutine(ActivateSunObjects());
    }

    IEnumerator AdjustCameraSize()
    {
        yield return new WaitForSeconds(7f);

        float startSize = Camera.orthographicSize;
        float endSize = 40f;
        float duration = 7f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Camera.orthographicSize = Mathf.Lerp(startSize, endSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.orthographicSize = endSize;

        startSize = Camera.orthographicSize;
        endSize = 80f;
        duration = 7f;
        elapsed = 0f;

        while (elapsed < duration)
        {
            Camera.orthographicSize = Mathf.Lerp(startSize, endSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.orthographicSize = endSize;
    }

    IEnumerator ActivateSunObjects()
    {
        yield return new WaitForSeconds(21f);

        for (int i = 0; i < 5; i++)
        {
            Sun[i].SetActive(true);
            yield return new WaitForSeconds(1f); // 각 오브젝트 활성화 사이에 1초 대기
        }

        for (int i = 5; i < 10; i++)
        {
            Sun[i].SetActive(true);
            yield return new WaitForSeconds(1f); // 각 오브젝트 활성화 사이에 1초 대기
        }

        yield return new WaitForSeconds(3.8f);

        yield return StartCoroutine(ChangeWhiteAlpha(0f, 255f / 255f, 3.4f));

    }

    IEnumerator ChangeWhiteAlpha(float startAlpha, float endAlpha, float duration)
    {
        SpriteRenderer whiteRenderer = white.GetComponent<SpriteRenderer>();
        if (whiteRenderer != null)
        {
            float elapsed = 0f;
            Color color = whiteRenderer.color;

            while (elapsed < duration)
            {
                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                whiteRenderer.color = new Color(color.r, color.g, color.b, newAlpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            whiteRenderer.color = new Color(color.r, color.g, color.b, endAlpha);
        }
    }
}