using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ChallengeGameManager;

public class ChDeadzone : MonoBehaviour
{
    ChallengeGameManager challengeGameManager;

    private void Start()
    {
        challengeGameManager = FindAnyObjectByType<ChallengeGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("P1ball"))
        {
            ChBallControl chBallControl = collision.GetComponent<ChBallControl>();

            if (!chBallControl.isExpanding)
            {
                GameData.CurrentScore = challengeGameManager.scorenum;
                SceneManager.LoadScene("ChallengeFail");
            }
        }
    }
}
