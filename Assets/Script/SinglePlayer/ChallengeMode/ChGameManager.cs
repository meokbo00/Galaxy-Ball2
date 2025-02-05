using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeGameManager : MonoBehaviour
{
    public GameObject P1ballPrefab;
    public GameObject[] FireItemPrefab;
    public GameObject P1firezone;
    public GameObject P1Itemsave;
    public GameObject fireitem;

    private Vector3 clickPosition;
    public bool isDragging = false;
    public static float shotDistance;
    public static Vector3 shotDirection;

    public int maxscorenum;
    public int scorenum;
    public TMP_Text maxscoretext;
    public TMP_Text scoretext;

    public static class GameData
    {
        public static int CurrentScore;
    }

    private int totalBalls; // 전체 공 개수 저장

    private void Start()
    {
        // 게임 시작 시 저장된 maxscorenum 불러오기
        maxscorenum = PlayerPrefs.GetInt("MaxScore", 0);
        maxscoretext.text = "Best : " + maxscorenum.ToString();

        P1firezone.SetActive(true);

        // 초기화
        UpdateTotalBalls();
    }

   

    private void Update()
    {
        // 점수가 변경되었을 때만 PlayerPrefs에 저장
        if (maxscorenum < scorenum)
        {
            maxscorenum = scorenum;
            PlayerPrefs.SetInt("MaxScore", maxscorenum);
            PlayerPrefs.Save();
        }

        maxscoretext.text = "Best : " + maxscorenum.ToString();
        scoretext.text = "Score : " + scorenum.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapPointAll(clickPosition);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == P1firezone)
                {
                    if (fireitem != null)
                    {
                        Instantiate(fireitem, clickPosition, Quaternion.identity);
                        Debug.Log("P1 아이템을 사용했습니다.");
                        Debug.Log("아이템 이름은 " + fireitem.gameObject.name + "입니다.");
                        isDragging = true;
                        fireitem = null;
                        break;
                    }
                    else
                    {
                        Instantiate(P1ballPrefab, clickPosition, Quaternion.identity);
                        Debug.Log("P1 기본 공을 생성했습니다.");
                        isDragging = true;
                        break;
                    }
                }
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            shotDistance = Vector3.Distance(clickPosition, currentPosition) * 2;
            Vector3 dragDirection = (currentPosition - clickPosition).normalized;
            shotDirection = dragDirection;
            isDragging = false;
        }

        // 총알 개수 업데이트
        UpdateTotalBalls();

        if (totalBalls > 15)
        {
            GameData.CurrentScore = scorenum;
            SceneManager.LoadScene("ChallengeFail");
        }
    }

    private void UpdateTotalBalls()
    {
        totalBalls = GameObject.FindGameObjectsWithTag("EnemyBall").Length +
                     GameObject.FindGameObjectsWithTag("P1ball").Length;
    }
}
