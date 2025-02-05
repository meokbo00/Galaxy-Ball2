using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ChallengeGameManager;

public class ELFailManager : MonoBehaviour
{
    public Button retry;
    public Button back;
    void Start()
    {
        this.retry.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("EndlessInGame");
        });
        this.back.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Start Scene");
        });

    }
}
