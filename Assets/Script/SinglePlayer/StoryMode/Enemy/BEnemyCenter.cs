using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BEnemyCenter : MonoBehaviour
{
    SPGameManager spGameManager;
    StageGameManager stagegameManager;
    BGMControl bGMControl;
    Rigidbody2D rigid;
    public float increase = 4f;
    public bool hasExpanded = false;
    public int durability;
    public int initialRandomNumber; // �ʱ� randomNumber ���� ������ ����
    public TextMeshPro textMesh;
    public Enemy1Fire[] enemy1Fires; // ���� Enemy1Fire ������ ���� �迭
    public bool isShowHP;
    public bool isHide;

    public int MaxHP;
    public int MinHP;
    public float MaxFireTime;
    public float MinFireTime;
    public float MaxAngle;
    public float MinAngle;
    public float fontsize;

    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string SPEndlessFName = "SPEndlessF(Clone)";


    private void Start()
    {
        stagegameManager = FindAnyObjectByType<StageGameManager>();
        spGameManager = FindAnyObjectByType<SPGameManager>();
        bGMControl = FindAnyObjectByType<BGMControl>();
        rigid = GetComponent<Rigidbody2D>();
        GameObject textObject = new GameObject("TextMeshPro");
        string scenename = SceneManager.GetActiveScene().name;
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        durability = Random.Range(MinHP, MaxHP);
        if (scenename == "EndlessInGame")
        {
            durability += (stagegameManager.ELRound * 2);
        }
        initialRandomNumber = durability; // �ʱ� randomNumber ���� ����
        if (isShowHP)
        {
            textMesh.text = durability.ToString();
        }
        textMesh.fontSize = fontsize;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 3;

        enemy1Fires = GetComponentsInChildren<Enemy1Fire>(); // Enemy1Fire ������Ʈ �迭 ����

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
        if (coll.gameObject.name != SPEndlessFName || coll.gameObject.name != SPTwiceFName)
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
            // 5�� ���� ����
            yield return new WaitForSeconds(Random.Range(MinFireTime, MaxFireTime));

            // ȸ���� ���� ����
            float targetAngle = Random.Range(MinAngle, MaxAngle);
            float currentAngle = transform.eulerAngles.z;
            float rotationTime = 1f; // ȸ���ϴ� �� �ɸ��� �ð�
            float elapsedTime = 0f;

            // ȸ���ϱ�
            while (elapsedTime < rotationTime)
            {
                elapsedTime += Time.deltaTime;
                float angle = Mathf.LerpAngle(currentAngle, targetAngle, elapsedTime / rotationTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
                yield return null;
            }

            // ȸ���� ���� �� 1�� �ڿ� �Ѿ� �߻�
            yield return new WaitForSeconds(1f);

            if (enemy1Fires != null)
            {
                foreach (var enemy1Fire in enemy1Fires)
                {
                    spGameManager.AddBall();
                    enemy1Fire.SpawnBullet();
                }
            }
        }
    }

    private void ChangeObjectColor(Transform parentTransform)
    {
        if (!isHide)
            return;

        // �ڱ� �ڽ��� ���� ����
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

        // �θ� ������Ʈ�� �ڽĵ��� �˻��ϸ鼭 ������ ����
        foreach (Transform child in parentTransform)
        {
            ChangeObjectColor(child);
        }
    }
}