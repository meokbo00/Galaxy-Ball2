using UnityEngine;
using UnityEngine.UI;

public class ItemUse : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            string destroyedIconTag = gameObject.tag;
            GameManager icontag = FindObjectOfType<GameManager>(); // 임시로 적용한 코드입니다.
            icontag.PrintDestroyedicontag(destroyedIconTag);
            Debug.Log("아이템 " + destroyedIconTag + " 을 클릭했습니다");
            Destroy(gameObject);
        });
    }
}