using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PracticeSceneBtn : MonoBehaviour
{
    public Button Back;
    //public AudioSource ButtonAudio;
    void Start()
    {
        Back.onClick.AddListener(BackClicked);
    }
    void BackClicked()
    {
        //ButtonAudio.Play();
        SceneManager.LoadScene("Start Scene");
    }
}
