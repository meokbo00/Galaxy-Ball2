using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TextWithDelay
{
    public string text;
    public float delay;
}

public class Chat
{
    public int id;
    public List<TextWithDelay> textWithDelay;
}

public class ShowText : MonoBehaviour
{
    public TMP_Text chatting;
    public GameObject ChatBox;
    public TextAsset KJsonFile;
    private List<Chat> chats;
    private int currentChatIndex = 0;
    private int currentTextIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private int chatIdToDisplay = -1;
    public int logTextIndex = 0; // 로그용 변수 추가

    public UnityEvent<int> OnChatComplete = new UnityEvent<int>();

    void Awake()
    {
        ChatBox.SetActive(true);

        // 무조건 KJsonFile 사용
        TextAsset selectedJsonFile = KJsonFile;

        if (selectedJsonFile != null)
        {
            try
            {
                var json = selectedJsonFile.text;
                chats = JsonConvert.DeserializeObject<List<Chat>>(json);
            }
            catch (JsonReaderException e)
            {
                Debug.LogError("JSON 파싱 실패: " + e.Message);
            }
        }
    }


    void OnEnable()
    {
        if (chatIdToDisplay != -1)
        {
            DisplayChatById(chatIdToDisplay);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                chatting.text = chats[currentChatIndex].textWithDelay[currentTextIndex].text;
                isTyping = false;
            }
            else
            {
                currentTextIndex++;
                if (currentTextIndex >= chats[currentChatIndex].textWithDelay.Count)
                {
                    currentTextIndex = 0;
                    if (chatIdToDisplay == -1)
                    {
                        currentChatIndex++;
                    }
                    else
                    {
                        ChatBox.SetActive(false);
                        OnChatComplete.Invoke(chats[currentChatIndex].id);
                        chatIdToDisplay = -1;
                        return;
                    }
                }
                DisplayCurrentChat();
            }
        }
    }

    private void DisplayCurrentChat()
    {
        if (currentChatIndex < chats.Count)
        {
            if (currentTextIndex < chats[currentChatIndex].textWithDelay.Count)
            {
                var textWithDelay = chats[currentChatIndex].textWithDelay[currentTextIndex];
                typingCoroutine = StartCoroutine(Typing(textWithDelay.text, textWithDelay.delay));

                // 로그 텍스트 인덱스 증가
                logTextIndex++;
            }
            else
            {
                ChatBox.SetActive(false);
                OnChatComplete.Invoke(chats[currentChatIndex].id);
            }
        }
        else
        {
            ChatBox.SetActive(false);
            OnChatComplete.Invoke(chats[currentChatIndex - 1].id);
        }
        Debug.Log("텍스트 순서 : " + logTextIndex);
    }

    IEnumerator Typing(string text, float delay)
    {
        isTyping = true;
        chatting.text = string.Empty;

        for (int i = 0; i < text.Length; i++)
        {
            chatting.text += text[i];
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
    }

    public void DisplayChatById(int chatId)
    {
        chatIdToDisplay = chatId;
        var chat = chats.Find(c => c.id == chatId);
        if (chat != null)
        {
            currentChatIndex = chats.IndexOf(chat);
            currentTextIndex = 0;
            DisplayCurrentChat();
        }
        else
        {
            Debug.LogError("Chat with ID " + chatId + " not found.");
        }
    }
}
