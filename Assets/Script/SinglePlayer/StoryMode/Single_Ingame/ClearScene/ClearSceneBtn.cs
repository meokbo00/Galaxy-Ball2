using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearSceneBtn : MonoBehaviour
{
    public Button button1;
    public Button button2;
    void Start()
    {
        this.button1.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Stage");
        });
        this.button2.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Stage");
        });
    }
}
