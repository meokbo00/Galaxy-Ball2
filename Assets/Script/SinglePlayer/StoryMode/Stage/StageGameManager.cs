using UnityEngine;

public class StageGameManager : MonoBehaviour
{
    public static StageGameManager instance = null;
    public int StageClearID;
    public int ELRound;
    public int ELnum;
    public float ELlevel;

    public bool firstTutorialShown;
    public bool secondTutorialShown;
    public bool isending = false;
    public bool isenglish = false;


    private int ELnumIDCache;
    private float ELlevelIDCache;
    private int ELRoundIDCache;
    private int stageClearIDCache; // PlayerPrefs 값을 캐싱
    private bool isEndingCache;
    private bool firstTutorialShownCache;
    private bool secondTutorialShownCache;
    private bool isenglishCache;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            ELRoundIDCache = PlayerPrefs.GetInt("ELRound", 1);
            ELnumIDCache = PlayerPrefs.GetInt("ELnum", 1);
            ELlevelIDCache = PlayerPrefs.GetFloat("ELlevel", 2);
            stageClearIDCache = PlayerPrefs.GetInt("StageClearID", 0);
            isEndingCache = PlayerPrefs.GetInt("IsEnding", 0) == 1;
            firstTutorialShownCache = PlayerPrefs.GetInt("FirstTutorialShown", 0) == 1;
            secondTutorialShownCache = PlayerPrefs.GetInt("SecondTutorialShown", 0) == 1;
            isenglishCache = PlayerPrefs.GetInt("isenglish", 0) == 1;

            ELRound = ELRoundIDCache;
            ELnum = ELnumIDCache;
            ELlevel = ELlevelIDCache;
            StageClearID = stageClearIDCache;
            isending = isEndingCache;
            firstTutorialShown = firstTutorialShownCache;
            secondTutorialShown = secondTutorialShownCache;
            isenglish = isenglishCache;
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }


    //초기화용 메서드

    //private void Start()
    //{
    //    ELnum = 1;
    //    ELlevel = 2;
    //    ELRound = 1;
    //    SaveELlevelAndELnum();
    //    StageClearID = 0;
    //    isending = false;
    //    SaveStageClearID();
    //    SaveIsEnding();
    //    notfirstTutosave();
    //    notsecendtutosave();
    //}


    public void PauseGame()
    {
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 일시정지 해제
    }

    public void SaveStageClearID()
    {
        if (stageClearIDCache != StageClearID)  // 값이 변경된 경우에만 저장
        {
            PlayerPrefs.SetInt("StageClearID", StageClearID);
            PlayerPrefs.Save();
            stageClearIDCache = StageClearID;  // 캐시 업데이트
        }
    }
    public void SaveELlevelAndELnum()
    {
        if (ELRoundIDCache != ELRound)
        {
            PlayerPrefs.SetInt("ELRound", ELRound);
            PlayerPrefs.Save();
            ELRoundIDCache = ELRound;
        }
        if (ELnumIDCache != ELnum)
        {
            PlayerPrefs.SetInt("ELnum", ELnum);
            PlayerPrefs.Save();
            ELnumIDCache = ELnum;
        }
        if (ELlevelIDCache != ELlevel)  // 값이 변경된 경우에만 저장
        {
            PlayerPrefs.SetFloat("ELlevel", ELlevel);
            PlayerPrefs.Save();
            ELlevelIDCache = ELlevel;
        }
    }
    public void firstTutosave()
    {
        if (firstTutorialShownCache != firstTutorialShown)
        {
            PlayerPrefs.SetInt("FirstTutorialShown", 1); // 첫 번째 튜토리얼을 본 것으로 저장
            PlayerPrefs.Save();
            firstTutorialShownCache = firstTutorialShown;
        }
    }
    public void secendtutosave()
    {
        if (secondTutorialShownCache != secondTutorialShown)
        {
            PlayerPrefs.SetInt("SecondTutorialShown", 1); // 두 번째 튜토리얼을 본 것으로 저장
            PlayerPrefs.Save();
            secondTutorialShownCache = secondTutorialShown;
        }
    }
    public void notfirstTutosave()
    {
        if (firstTutorialShownCache != firstTutorialShown)
        {
            PlayerPrefs.SetInt("FirstTutorialShown", 0);
            PlayerPrefs.Save();
            firstTutorialShownCache = firstTutorialShown;
        }
    }
    public void notsecendtutosave()
    {
        if (secondTutorialShownCache != secondTutorialShown)
        {
            PlayerPrefs.SetInt("SecondTutorialShown", 0);
            PlayerPrefs.Save();
            secondTutorialShownCache = secondTutorialShown;
        }
    }
    public void SaveIsisenglish()
    {
        if (isenglishCache != isenglish)  // 값이 변경된 경우에만 저장
        {
            PlayerPrefs.SetInt("isenglish", isenglish ? 1 : 0);
            PlayerPrefs.Save();
            isenglishCache = isenglish;  // 캐시 업데이트
        }
    }
    public void SaveIsEnding()
    {
        if (isEndingCache != isending)  // 값이 변경된 경우에만 저장
        {
            PlayerPrefs.SetInt("IsEnding", isending ? 1 : 0);
            PlayerPrefs.Save();
            isEndingCache = isending;  // 캐시 업데이트
        }
    }
}
