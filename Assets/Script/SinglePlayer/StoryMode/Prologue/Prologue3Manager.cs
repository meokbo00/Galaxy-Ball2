using System.Collections;
using UnityEngine;

public class Prologue3Manager : MonoBehaviour
{
    public GameObject[] Circles;
    public Camera Camera;

    void Start()
    {
        StartCoroutine(ActivateCirclesRandomly());
    }

    IEnumerator ActivateCirclesRandomly()
    {
        yield return new WaitForSeconds(5f); // 5초 대기

        ShuffleArray(Circles);

        float activationInterval = 8f / Circles.Length; // 활성화 간격 계산

        for (int i = 0; i < Circles.Length; i++)
        {
            Circles[i].SetActive(true);
            yield return new WaitForSeconds(activationInterval);
        }

        yield return new WaitForSeconds(2f); // 2초 대기

        // 7.5초 동안 카메라 사이즈를 15로 이동
        float startSize = Camera.orthographicSize;
        float endSize = 15f;
        float duration = 7.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Camera.orthographicSize = Mathf.Lerp(startSize, endSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.orthographicSize = endSize;
    }

    private void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = temp;
        }
    }
}