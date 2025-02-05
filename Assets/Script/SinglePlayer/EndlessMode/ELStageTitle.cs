using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ELStageTitle : MonoBehaviour
{
    StageGameManager gameManager;
    public TMP_Text StageTitle;
    void Start()
    {
        gameManager = FindAnyObjectByType<StageGameManager>();
        StageTitle.text = "Round : " + gameManager.ELRound.ToString() +"       Stage : " + gameManager.ELnum.ToString();
    }
}
