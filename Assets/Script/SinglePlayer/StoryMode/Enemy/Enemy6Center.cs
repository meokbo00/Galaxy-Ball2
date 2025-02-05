using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy6center : MonoBehaviour
{
    StageGameManager stagegameManager;
    SPGameManager spGameManager;
    BGMControl bGMControl;
    public float increase = 4f;
    public bool hasExpanded = false;
    public int durability;
    private TextMeshPro textMesh;
    public Enemy1Fire[] enemy1Fires; // ���� Enemy1Fire ������ ���� �迭
    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string SPEndlessFName = "SPEndlessF(Clone)";
    public int MaxHP;
    public int MinHP;
    public float MaxFireTime;
    public float MinFireTime;

    private void Start()
    {
        stagegameManager = FindAnyObjectByType<StageGameManager>();
        spGameManager = FindObjectOfType<SPGameManager>();
        bGMControl = FindObjectOfType<BGMControl>();
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        string scenename = SceneManager.GetActiveScene().name;
        durability = Random.Range(MinHP, MaxHP);
        if (scenename == "EndlessInGame")
        {
            durability += stagegameManager.ELRound;
        }
        textMesh.text = durability.ToString();
        textMesh.fontSize = 6;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.autoSizeTextContainer = true;
        textMesh.rectTransform.localPosition = Vector3.zero;
        textMesh.sortingOrder = 3;

        enemy1Fires = GetComponentsInChildren<Enemy1Fire>(); // Enemy1Fire ������Ʈ �迭 ����

        StartCoroutine(RotateObject());
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "EnemyBall") return;
        if (coll.gameObject.tag == "Gojung") return;
        if (coll.gameObject.tag == "Wall") return;
        if (coll.gameObject.tag == "EnemyCenter") return;


        if (coll.gameObject.name != SPEndlessFName)
        {
            TakeDamage(1);
        }
        if (coll.gameObject.name == SPTwiceFName)
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage(int damage)
    {
        durability -= damage;
        textMesh.text = durability.ToString();
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
            yield return new WaitForSeconds(Random.Range(MinFireTime, MaxFireTime));

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
}