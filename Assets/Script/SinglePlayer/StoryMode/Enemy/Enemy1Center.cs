using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1center : MonoBehaviour
{
    StageGameManager stagegameManager;
    SPGameManager spGameManager;
    BGMControl bGMControl;
    public float increase = 4f;
    public bool hasExpanded = false;
    public int durability;
    public int initialRandomNumber; // 초기 randomNumber 값을 저장할 변수
    public TextMeshPro textMesh;
    public Enemy1Fire[] enemy1Fires; // 여러 Enemy1Fire 참조를 위한 배열
    public bool isShowHP;
    public bool isHide;
    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string SPEndlessFName = "SPEndlessF(Clone)";
    public int MaxHP;
    public int MinHP;
    public float MaxFireTime;
    public float MinFireTime;
    public float MaxAngle;
    public float MinAngle;
    public float fontsize;

    private void Start()
    {
        stagegameManager = FindAnyObjectByType<StageGameManager>();
        spGameManager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        string scenename = SceneManager.GetActiveScene().name;
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        durability = Random.Range(MinHP, MaxHP);
        if(scenename == "EndlessInGame")
        {
            durability += stagegameManager.ELRound;
        }
        initialRandomNumber = durability; // 초기 randomNumber 값을 저장
        if (isShowHP)
        {
            textMesh.text = durability.ToString();
        }
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 3;

        enemy1Fires = GetComponentsInChildren<Enemy1Fire>(); // Enemy1Fire 컴포넌트 배열 참조

        StartCoroutine(RotateObject());

        if (isHide)
        {
            ChangeObjectColor(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Untagged") return;
        if (coll.gameObject.tag == "EnemyBall") return;
        if (coll.gameObject.tag == "Gojung") return;
        if (coll.gameObject.name != SPEndlessFName && coll.gameObject.name != SPTwiceFName)
        {
            TakeDamage(1);
        }
        if (coll.gameObject.name == SPTwiceFName)
        {
            TakeDamage(2);
        }
    }
    void TakeDamage(int damage)
    {
        durability -= damage;
        if (isShowHP)
        {
            textMesh.text = durability.ToString();
        }
        if (durability <= 0)
        {
            if (bGMControl.SoundEffectSwitch)
            {
                bGMControl.SoundEffectPlay(4);
            }
            spGameManager.RemoveEnemy();
            Destroy(gameObject);
        }
    }
    private IEnumerator RotateObject()
    {
        while (true)
        {
            // 랜덤한 대기 시간
            yield return new WaitForSeconds(Random.Range(MinFireTime, MaxFireTime));

            // 회전할 각도 설정
            float targetAngle = Random.Range(MinAngle, MaxAngle);
            float currentAngle = transform.eulerAngles.z;
            float rotationTime = 1f; // 회전하는 데 걸리는 시간
            float elapsedTime = 0f;

            // 회전하기
            while (elapsedTime < rotationTime)
            {
                elapsedTime += Time.deltaTime;
                float angle = Mathf.LerpAngle(currentAngle, targetAngle, elapsedTime / rotationTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
                yield return null;
            }

            // 회전이 끝난 후 총알 발사
            FireBullets();
        }
    }

    // 총알 발사 메서드
    private void FireBullets()
    {
        if (enemy1Fires != null)
        {
            foreach (var enemy1Fire in enemy1Fires)
            {
                spGameManager.AddBall();
                enemy1Fire.SpawnBullet();
            }
        }
    }


    private void ChangeObjectColor(Transform parentTransform)
    {
        if (!isHide)
            return;

        // 자기 자신의 색상 변경
        SpriteRenderer currentSpriteRenderer = parentTransform.GetComponent<SpriteRenderer>();
        if (currentSpriteRenderer != null)
        {
            GameObject background = GameObject.Find("BackGround");
            if (background != null)
            {
                SpriteRenderer backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
                if (backgroundSpriteRenderer != null)
                {
                    currentSpriteRenderer.color = backgroundSpriteRenderer.color;
                }
            }
        }

        // 부모 오브젝트의 자식들을 검사하면서 색상을 변경
        foreach (Transform child in parentTransform)
        {
            ChangeObjectColor(child);
        }
    }
}