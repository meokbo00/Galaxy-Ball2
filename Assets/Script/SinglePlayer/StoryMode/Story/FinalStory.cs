using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalStory : MonoBehaviour
{
    ShowText showText;
    StageGameManager gameManager;
    TextManager textManager;
    public Camera camera;
    public GameObject play;
    public GameObject Fadein;
    private Image fadeinImage;

    void Start()
    {
        textManager = FindObjectOfType<TextManager>();
        gameManager = FindObjectOfType<StageGameManager>();
        fadeinImage = Fadein.GetComponent<Image>();

        if (gameManager.StageClearID == 66)
        {
            textManager.GiveMeTextId(2);
        }
    }

    void Update()
    {
        if (gameManager.StageClearID == 66)
        {
            showText = FindObjectOfType<ShowText>();
            if (showText.logTextIndex == 9)
            {
                StartCoroutine(FadeOut(fadeinImage, 3f));
            }
            if (showText.logTextIndex == 14)
            {
                StartCoroutine(ChangeCameraSize(camera, 10000f, 10f));
            }
        }
    }

    IEnumerator FadeOut(Image image, float duration)
    {
        Color startColor = image.color;
        float startAlpha = startColor.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time / duration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    IEnumerator ChangeCameraSize(Camera camera, float targetSize, float duration)
    {
        float startSize = camera.orthographicSize;
        Vector3 startScale = play.transform.localScale;
        Vector3 targetScale = new Vector3(0.1f, 0.1f, 0.1f);
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            camera.orthographicSize = Mathf.Lerp(startSize, targetSize, time / duration);
            play.transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            yield return null;
        }

        camera.orthographicSize = targetSize;
        play.transform.localScale = targetScale;

        // Wait for 3 seconds
        yield return new WaitForSeconds(5f);

        // Load the Ending scene
        if (!gameManager.isenglish)
        {
            SceneManager.LoadScene("Ending");
        }
        else
        {
            SceneManager.LoadScene("EEnding");
        }
    }
}
