using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject Linebox;

    public void GiveMeTextId(int chatId)
    {
        if (Linebox != null)
        {
            Linebox.SetActive(true); 

            ShowText showTextScript = Linebox.GetComponent<ShowText>();
            if (showTextScript != null)
            {
                showTextScript.DisplayChatById(chatId);
            }
        }
    }
}