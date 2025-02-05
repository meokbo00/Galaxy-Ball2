using UnityEngine;
using TMPro;

public class CameraReduction : MonoBehaviour
{
    private Camera mainCamera;
    private int currentIndex = 0;
    private float[] sizes;
    private string[] sizeTexts = { "100%", "75%", "50%", "15%", "1%" };

    public TextMeshProUGUI buttonText;
    public int gear; // Public으로 선언된 gear 변수

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다!");
            return;
        }

        sizes = new float[] { 4f, 7f, 15f, 30f, 45f };
        UpdateGear(); // gear 값 업데이트
        UpdateButtonText(); // 초기 텍스트 업데이트
    }

    public void ChangeCameraSize()
    {
        if (mainCamera != null)
        {
            currentIndex = (currentIndex + 1) % sizes.Length;
            mainCamera.orthographicSize = sizes[currentIndex]; // 카메라 크기 변경
            UpdateGear(); // gear 값 업데이트
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = sizeTexts[currentIndex];
        }
    }

    private void UpdateGear()
    {
        // currentIndex가 0부터 시작하므로 gear는 1부터 시작하도록 설정
        gear = currentIndex + 1;
    }
}
