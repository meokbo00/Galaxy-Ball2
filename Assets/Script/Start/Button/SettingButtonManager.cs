using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingButtonManager : MonoBehaviour
{
    public Button BGM_ON;
    public Button BGM_OFF;
    public Button Sound_Effect_ON;
    public Button Sound_Effect_OFF;
    public Button Credit;
    public Button Back;
    public Button korea;
    public Button english;
    public AudioSource ButtonAudio;

    private BGMControl bGMControl;
    StageGameManager stageGameManager;
    private void Start()
    {
        stageGameManager = FindAnyObjectByType<StageGameManager>();
        bGMControl = FindObjectOfType<BGMControl>();
        LoadButtonStates();

        SetupButton(BGM_ON, BGM_OFF, () => {
            bGMControl.BGMSwitch = false;
            bGMControl.SaveAudioSettings(); // 상태 저장
        });
        SetupButton(BGM_OFF, BGM_ON, () => {
            bGMControl.BGMSwitch = true;
            bGMControl.SaveAudioSettings(); // 상태 저장
        });
        SetupButton(Sound_Effect_ON, Sound_Effect_OFF, () => {
            bGMControl.SoundEffectSwitch = false;
            bGMControl.SaveAudioSettings(); // 상태 저장
        });
        SetupButton(Sound_Effect_OFF, Sound_Effect_ON, () => {
            bGMControl.SoundEffectSwitch = true;
            bGMControl.SaveAudioSettings(); // 상태 저장
        });

        Credit.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            SceneManager.LoadScene("Credit Scene");
        });

        Back.onClick.AddListener(() =>
        {
            ButtonAudio.Play();
            SceneManager.LoadScene("Start Scene");
        });
        korea.onClick.AddListener(() =>
        {
            stageGameManager.isenglish = false;
            stageGameManager.SaveIsisenglish();
        });
        english.onClick.AddListener(() =>
        {
            stageGameManager.isenglish = true;
            stageGameManager.SaveIsisenglish();
        });
    }

    private void SetupButton(Button onButton, Button offButton, Action action)
    {
        onButton.onClick.AddListener(() =>
        {
            action();
            ButtonAudio.Play();
            onButton.gameObject.SetActive(false);
            offButton.gameObject.SetActive(true);
            SaveButtonStates();
        });
    }

    private void SaveButtonStates()
    {
        PlayerPrefs.SetInt("BGM_ON", BGM_ON.gameObject.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("BGM_OFF", BGM_OFF.gameObject.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("Sound_Effect_ON", Sound_Effect_ON.gameObject.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("Sound_Effect_OFF", Sound_Effect_OFF.gameObject.activeSelf ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadButtonStates()
    {
        BGM_ON.gameObject.SetActive(PlayerPrefs.GetInt("BGM_ON", 1) == 1);
        BGM_OFF.gameObject.SetActive(PlayerPrefs.GetInt("BGM_OFF", 0) == 1);
        Sound_Effect_ON.gameObject.SetActive(PlayerPrefs.GetInt("Sound_Effect_ON", 1) == 1);
        Sound_Effect_OFF.gameObject.SetActive(PlayerPrefs.GetInt("Sound_Effect_OFF", 0) == 1);
    }
}
