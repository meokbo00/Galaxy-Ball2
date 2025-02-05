using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject P1ballPrefab;
    public GameObject P2ballPrefab;
    public GameObject[] FireItemPrefab;

    public GameObject P1firezone;
    public GameObject P2firezone;

    public GameObject fireitem;

    private Vector3 clickPosition;
    public bool isDragging = false;
    public static float shotDistance;
    public static Vector3 shotDirection;

    public bool P1FireMode = true;
    public bool P2FireMode = false;
    private void Start()
    {
        P1firezone.gameObject.SetActive(true);
        P2firezone.gameObject.SetActive(true);
    }
    public void PrintDestroyedicontag(string icontag)
    {
        this.fireitem = null;
        switch (icontag)
        {
            case "Item_Big": fireitem = FireItemPrefab[0]; break;
            case "Item_Small": fireitem = FireItemPrefab[1]; break;
            case "Item_Twice": fireitem = FireItemPrefab[2]; break;
            case "Item_Invincible": fireitem = FireItemPrefab[3]; break;
            case "Item_BlackHole": fireitem = FireItemPrefab[4]; break;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0f;
            Collider2D[] colliders = Physics2D.OverlapPointAll(clickPosition);
            foreach (Collider2D collider in colliders)
            {
                if (P1FireMode && collider.gameObject == P1firezone)
                {
                    if (fireitem != null)
                    {
                        fireitem.gameObject.tag = "P1Item";
                        Instantiate(fireitem, clickPosition, Quaternion.identity);
                        Debug.Log("P1이 아이템을 사용하였습니다");
                        Debug.Log("아이템의 이름은 " + fireitem.gameObject.name + "입니다");
                    }
                    else
                    {
                        Instantiate(P1ballPrefab, clickPosition, Quaternion.identity);
                        Debug.Log("P1이 기본구체를 날렸습니다");
                    }
                    P1FireMode = false;
                    P2FireMode = true;
                    isDragging = true;
                    P1firezone.gameObject.SetActive(false);
                    P2firezone.gameObject.SetActive(true);
                    fireitem = null;
                    break;
                }
                if (P2FireMode && collider.gameObject == P2firezone)
                {
                    if (fireitem != null)
                    {
                        fireitem.gameObject.tag = "P2Item";
                        Instantiate(fireitem, clickPosition, Quaternion.identity);
                        Debug.Log("P2가 아이템을 사용하였습니다");
                    }
                    else
                    {
                        Instantiate(P2ballPrefab, clickPosition, Quaternion.identity);
                        Debug.Log("P2가 기본구체를 날렸습니다");
                    }
                    P1FireMode = true;
                    P2FireMode = false;
                    isDragging = true;
                    P1firezone.gameObject.SetActive(true);
                    P2firezone.gameObject.SetActive(false);
                    fireitem = null;
                    break;
                }
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            shotDistance = Vector3.Distance(clickPosition, currentPosition)*2;
            Vector3 dragDirection = (currentPosition - clickPosition).normalized;
            shotDirection = dragDirection;
            isDragging = false;
        }
    }

}