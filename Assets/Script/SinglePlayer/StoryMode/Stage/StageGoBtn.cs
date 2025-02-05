using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageGoBtn : MonoBehaviour
{
    public Button GoBtn;
    public GameObject Player;
    StageGameManager gameManager;
    StageState state;

    private void Start()
    {
        state = FindObjectOfType<StageState>();
        gameManager = FindObjectOfType<StageGameManager>();
        int choosestage = state.stagenum;
        this.GoBtn.onClick.AddListener(() =>
        {
            // 씬을 로드하기 전에 chooseStage 값을 저장
            GlobalData.SelectedStage = StageState.chooseStage;

            // 씬을 로드하기 전에 플레이어의 위치를 저장
            if (Player != null)
            {
                GlobalData.PlayerPosition = new Vector2(Player.transform.position.x, Player.transform.position.y);
            }
            if (gameManager.StageClearID == 65 && StageState.chooseStage == 65)
            {
                if (!gameManager.isenglish)
                {
                    SceneManager.LoadScene("Final-InGame");
                }
                else
                {
                    SceneManager.LoadScene("EFinal-InGame");
                }
            }
            else
            {
                SceneManager.LoadScene("Story-InGame");
            }
        });
    }
}
public static class GlobalData
{
    public static int SelectedStage;
    public static Vector2 PlayerPosition; // 플레이어의 위치를 저장할 변수 추가
}
