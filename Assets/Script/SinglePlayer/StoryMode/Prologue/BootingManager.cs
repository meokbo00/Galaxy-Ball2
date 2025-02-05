using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BootingManager : MonoBehaviour
{
    public GameObject videoPlayerObject; // Video Player�� ������ ���� ������Ʈ�� �ν����Ϳ��� ����

    private VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayerObject != null)
        {
            videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached += OnVideoEnd;
            }
            else
            {
                Debug.LogError("Video Player component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("Video Player GameObject is not assigned.");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // ������ ������ ȣ��Ǵ� �ݹ�
        StartCoroutine(LoadPrologueAfterDelay(3f));
    }

    IEnumerator LoadPrologueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Prologue");
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}