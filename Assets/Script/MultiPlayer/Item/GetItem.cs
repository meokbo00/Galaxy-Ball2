using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 날린 구체가 생성된 아이템에 부딫히면 사라진뒤 해당 태그를 ShowP1, P2 Item 에 보내는 스크립트
public class GetItem : MonoBehaviour
{
    BGMControl bGMControl;

    private void Start()
    {
        bGMControl = FindObjectOfType<BGMControl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if ((collision.gameObject.tag == "P1ball" || collision.gameObject.tag == "P1Item" || collision.gameObject.tag == "Item") && otherRigidbody != null)
        {
            bGMControl.SoundEffectPlay(3);
            string destroyedObjecttag = gameObject.tag;
            ShowP1ItemIcon itemIconScript = FindObjectOfType<ShowP1ItemIcon>();
            itemIconScript.PrintDestroyedObjectTag(destroyedObjecttag);
            Debug.Log("P1이 아이템 " + destroyedObjecttag + "를 획득했습니다");
            Destroy(gameObject);
        }
        if((collision.gameObject.tag == "P2ball" || collision.gameObject.tag == "P2Item") && otherRigidbody != null)
        {
            bGMControl.SoundEffectPlay(3);
            string destroyedObjecttag = gameObject.tag;
            ShowP2ItemIcon itemIconScript = FindObjectOfType<ShowP2ItemIcon>();
            itemIconScript.PrintDestroyedObjectTag(destroyedObjecttag);
            Debug.Log("P2가 아이템 " + destroyedObjecttag + "를 획득했습니다");
            Destroy(gameObject);
        }
    }
}
