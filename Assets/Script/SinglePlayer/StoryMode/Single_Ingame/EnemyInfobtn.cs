using UnityEngine;
using UnityEngine.UI;

public class EnemyInfobtn : MonoBehaviour
{
    private StageGameManager stageGameManager;
    public Button okbtn;

    void Awake()
    {
        stageGameManager = FindObjectOfType<StageGameManager>();
    }

    private void Start()
    {
        this.okbtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    void OnEnable()
    {
        stageGameManager.PauseGame();
    }

    void OnDisable()
    {
        stageGameManager.ResumeGame();
    }
}