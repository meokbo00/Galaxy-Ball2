using System.Collections;
using TMPro;
using UnityEngine;

public class RemainTime : MonoBehaviour
{
    public TMP_Text timeDisplay;
    StageGameManager stageGameManager;
    public int years;
    public int days;
    public int hours;
    public int minutes;
    public int seconds;

    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            DisplayTime();
            yield return new WaitForSeconds(1f);
            DecreaseTime();
        }
    }

    void DisplayTime()
    {
        stageGameManager = FindAnyObjectByType<StageGameManager>();

        if (!stageGameManager.isenglish)
        {
            timeDisplay.text = $"개발자의 코드 수정까지\n{years}년 {days}일 {hours}시간 {minutes}분 {seconds}초";
        }
        else
        {
            timeDisplay.text = $"Until developers fix the code\n{years}y {days}d {hours}h {minutes}m {seconds}s";
        }
    }

    void DecreaseTime()
    {
        if (seconds > 0)
        {
            seconds--;
        }
        else
        {
            if (minutes > 0)
            {
                minutes--;
                seconds = 59;
            }
        }
    }
}