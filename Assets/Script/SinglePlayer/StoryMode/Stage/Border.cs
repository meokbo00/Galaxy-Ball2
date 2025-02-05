using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public GameObject ch2Border;
    public GameObject ch3Border;
    void Update()
    {
        StageGameManager gameManager = FindObjectOfType<StageGameManager>();
        if(gameManager.StageClearID > 15)
        {
            Destroy(ch2Border);
        }
        if(gameManager.StageClearID >= 65)
        {
            Destroy(ch3Border);
        }
    }
}
