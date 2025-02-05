using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayBGM : MonoBehaviour
{
    public AudioSource[] BGM; // 오디오 소스를 배열로 변경
    private BGMControl bGMControl;

    void Start()
    {
        bGMControl = FindAnyObjectByType<BGMControl>();

        if (bGMControl != null)
        {
            if (bGMControl.BGMSwitch)
            {
                // StageClearID 값에 따라 다른 오디오 소스를 재생
                if (StageState.chooseStage == 25 || StageState.chooseStage == 45)
                {
                    if (BGM.Length > 1) // 두 번째 오디오 소스가 존재하는지 확인
                    {
                        BGM[1].Play();
                    }
                }
                else if(StageState.chooseStage == 65)
                {
                    BGM[2].Play();
                }
                else
                {
                    if (BGM.Length > 0) // 첫 번째 오디오 소스가 존재하는지 확인
                    {
                        BGM[0].Play();
                    }
                }
            }
            else
            {
                foreach (var audioSource in BGM)
                {
                    if (audioSource != null)
                    {
                        audioSource.Stop();
                    }
                }
            }
        }
    }
}
