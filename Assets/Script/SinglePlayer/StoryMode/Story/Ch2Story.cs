using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2Story : MonoBehaviour
{
    public Camera Camera;
    public GameObject Fadein;
    StageGameManager stageGameManager;
    StageBallController stageBallController;
    TextManager textManager;
    ShowText showText;
    public GameObject navigation;

    void Start()
    {
        showText = FindObjectOfType<ShowText>();
        textManager = FindObjectOfType<TextManager>();
        stageGameManager = FindObjectOfType<StageGameManager>();
        stageBallController = FindObjectOfType<StageBallController>();


        switch (stageGameManager.StageClearID)
        {
            case 6:
                textManager.GiveMeTextId(1);
                Fadein.SetActive(true);
                Camera.orthographicSize = 150;

                if (stageBallController != null)
                {
                    stageBallController.enabled = false;
                }
                break;
            case 7:
                textManager.GiveMeTextId(2);
                break;
            case 16:
                textManager.GiveMeTextId(3);
                break;
            case 19:
                textManager.GiveMeTextId(4);
                break;
            case 25:
                textManager.GiveMeTextId(5);
                break;
            case 29:
                textManager.GiveMeTextId(6);
                break;
            case 33:
                textManager.GiveMeTextId(7);
                break;
            case 45:
                textManager.GiveMeTextId(8);
                break;
            case 57:
                textManager.GiveMeTextId(9);
                break;
            case 65:
                navigation.SetActive(false);
                textManager.GiveMeTextId(10);
                break;
            case 66:
                navigation.SetActive(false);
                textManager.GiveMeTextId(11);
                break;
        }
    }

    void FixedUpdate()
    {
        if (stageGameManager.StageClearID == 6)
        {
            showText = FindObjectOfType<ShowText>();
            if (showText.logTextIndex == 3)
            {
                Fadein.SetActive(false);
            }
            if (showText.logTextIndex == 4)
            {
                StartCoroutine(ChangeCameraSize(150, 4, 5f)); 
            }
            if (showText.logTextIndex == 36)
            {
                stageBallController.enabled = true;
            }
        }
        //if (stageGameManager.StageClearID == 66 && showText.logTextIndex == 3)
        //{
        //    stageGameManager.StageClearID = 67;
        //    stageGameManager.SaveStageClearID();
        //}
    }

    IEnumerator ChangeCameraSize(float fromSize, float toSize, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Camera.orthographicSize = Mathf.Lerp(fromSize, toSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.orthographicSize = toSize;
    }
}
