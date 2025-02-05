using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SImsCenter : MonoBehaviour
{
    SPGameManager sPGameManager;
    BGMControl bGMControl;
    public int durability;
    public int initialRandomNumber; // 초기 randomNumber 값을 저장할 변수
    public TextMeshPro textMesh;
    public bool isShowHP;
    public float fontsize;
    private const string SPTwiceFName = "SPTwiceF(Clone)";
    private const string SPEndlessFName = "SPEndlessF(Clone)";
    public int MaxHP;
    public int MinHP;
    private void Start()
    {
        sPGameManager = FindObjectOfType<SPGameManager>();
        bGMControl = FindObjectOfType<BGMControl>();
        GameObject textObject = new GameObject("TextMeshPro");
        textObject.transform.parent = transform;
        textMesh = textObject.AddComponent<TextMeshPro>();
        durability = Random.Range(MinHP, MaxHP);
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


    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "EnemyBall") return;
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
            sPGameManager.RemoveEnemy();
            Destroy(gameObject);
        }
    }
}
