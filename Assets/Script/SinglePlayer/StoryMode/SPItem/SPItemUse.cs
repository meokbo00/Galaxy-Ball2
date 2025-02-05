using UnityEngine;
using UnityEngine.UI;

public class SPItemUse : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            string destroyedIconTag = gameObject.tag;
            SPGameManager icontag = FindObjectOfType<SPGameManager>(); // �ӽ÷� ������ �ڵ��Դϴ�.
            icontag.PrintDestroyedicontag(destroyedIconTag);
            Debug.Log("������ " + destroyedIconTag + " �� Ŭ���߽��ϴ�");
            Destroy(gameObject);
        });
    }
}