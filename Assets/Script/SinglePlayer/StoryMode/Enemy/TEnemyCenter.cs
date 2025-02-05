using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEnemyCenter : MonoBehaviour
{
    StageGameManager stagegameManager;
    SPGameManager spgameManager;
    BGMControl bGMControl;
    public int durability;
    public int initialRandomNumber; // 초기 randomNumber 값을 저장할 변수
    public TextMeshPro textMesh;
    public bool isShowHP;
    public bool isHide;
    public float fontsize;
    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string SPEndlessFName = "SPEndlessF(Clone)";
    public int MaxHP;
    public int MinHP;
    private void Start()
    {
        spgameManager = FindAnyObjectByType<SPGameManager>();
        stagegameManager = FindAnyObjectByType<StageGameManager>();
        bGMControl = FindObjectOfType<BGMControl>();
        string scenename = SceneManager.GetActiveScene().name;
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        durability = Random.Range(MinHP, MaxHP);
        if (scenename == "EndlessInGame")
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


        if (isHide)
        {
            ChangeObjectColor(transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "EnemyBall") return;
        if (coll.gameObject.tag == "Gojung") return;
        if (coll.gameObject.name != SPEndlessFName)
        {
            TakeDamage(1);
        }
        if (coll.gameObject.name == SPTwiceFName)
        {
            TakeDamage(1);
        }
    }
    void TakeDamage(int damage)
    {
        string scenename = SceneManager.GetActiveScene().name;
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
            Destroy(gameObject);
            if (scenename == "EndlessInGame")
            {
                spgameManager.RemoveEnemy();
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
