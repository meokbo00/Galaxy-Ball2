using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartButtonManager : MonoBehaviour
{
    StageGameManager stageGameManager;
    public GameObject isreally2p;

    public GameObject p1orp2;
    public GameObject singleSetting;
    public Button Play;
    public Button Setting;
    public Button Quit;

    public Button singleP;
    public Button multiP;
    public Button x;
    public Button p2yes;
    public Button p2no;

    public AudioSource ButtonAudio;

    void Start()
    {
        stageGameManager = FindAnyObjectByType<StageGameManager>();
        this.Play.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            p1orp2.gameObject.SetActive(true);
        });

        this.Setting.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            SceneManager.LoadScene("Setting Scene");
        });

        this.Quit.onClick.AddListener(() =>
        {
            Debug.Log("게임 종료!");
            ButtonAudio.Play();
            Application.Quit();
        });

        this.singleP.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            singleSetting.gameObject.SetActive(true);
        });

        this.multiP.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            if ((stageGameManager.StageClearID == 0) || (stageGameManager.StageClearID == 1))
            {
                isreally2p.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("Main Scene");
            }
        });
        p2yes.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main Scene");
        });
        p2no.onClick.AddListener(() =>
        {
            isreally2p.SetActive(false);
        });
        this.x.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.p1orp2.gameObject.SetActive(false);
        });
    }
}