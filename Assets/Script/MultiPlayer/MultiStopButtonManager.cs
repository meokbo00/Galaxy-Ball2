using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 멀티플레이어에 있는 여러가지 버튼들을 관리하는 스크립트입니다.
public class MultiStopButtonManager : MonoBehaviour
{
    public Button Stopbtn;
    public Button Xbtn;
    public Button Resume;
    public Button Restart;
    public Button BacktoStage;
    public Button Main_menu;

    public GameObject Stop_Channel;
    public AudioSource ButtonAudio;
    StageGameManager stageGameManager;
    bool ispause;

    void Start()
    {
        stageGameManager = FindObjectOfType<StageGameManager>();
        ispause = false;
        this.Stopbtn.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(true);

            Time.timeScale = 0;
            ispause = true;
        });
        this.Xbtn.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;
            return;
        });
        this.Resume.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;
            return;
        });
        this.Restart.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;

            // 현재 씬 이름을 확인하여 조건에 따라 다른 씬을 로드
            if (SceneManager.GetActiveScene().name == "ChallengeScene")
            {
                SceneManager.LoadScene("ChallengeScene");
            }
            if (SceneManager.GetActiveScene().name == "Story-InGame")
            {
                SceneManager.LoadScene("Story-InGame");
            }
            if (SceneManager.GetActiveScene().name == "Main Scene")
            {
                SceneManager.LoadScene("Main Scene");
            }
            if (SceneManager.GetActiveScene().name == "EndlessInGame")
            {
                SceneManager.LoadScene("EndlessInGame");
            }
        });

        // 현재 씬 이름이 "ChallengeScene"이 아니면 BacktoStage 버튼에 이벤트 추가
        if (SceneManager.GetActiveScene().name != "ChallengeScene")
        {
            if (BacktoStage != null)
            {
                this.BacktoStage.onClick.AddListener(() =>
                {
                    ButtonAudio.Play();
                    this.Stop_Channel.gameObject.SetActive(false);

                    Time.timeScale = 1;
                    ispause = false;

                    if (stageGameManager.StageClearID < 6)
                    {
                        if (!stageGameManager.isenglish)
                        {
                            SceneManager.LoadScene("Stage");
                        }
                        else
                        {
                            SceneManager.LoadScene("EStage");
                        }
                    }
                    else if (stageGameManager.StageClearID >= 6)
                    {
                        if (!stageGameManager.isenglish)
                        {
                            SceneManager.LoadScene("Main Stage");
                        }
                        else
                        {
                            SceneManager.LoadScene("EMain Stage");
                        }
                    }
                });
            }
        }

        this.Main_menu.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            this.Stop_Channel.gameObject.SetActive(false);

            Time.timeScale = 1;
            ispause = false;

            SceneManager.LoadScene("Start Scene");
        });
    }
}
