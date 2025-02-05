using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMControl : MonoBehaviour
{
    public AudioSource[] SoundEffect;
    public bool BGMSwitch = true;
    public bool SoundEffectSwitch = true;

    private void Awake()
    {
        // 게임 시작 시 저장된 상태를 불러오기
        LoadAudioSettings();
        UpdateAudioSources(); // 초기 상태 반영
    }

    // 소스의 활성화 상태를 업데이트합니다.
    public void UpdateAudioSources()
    {
        ToggleAudioSources(SoundEffect, SoundEffectSwitch);
    }

    // 소스의 활성화 상태를 토글합니다.
    void ToggleAudioSources(AudioSource[] sources, bool isEnabled)
    {
        foreach (var source in sources)
        {
            if (isEnabled && !source.enabled)
            {
                source.enabled = true; // 활성화 상태로 변경
            }
            else if (!isEnabled && source.isPlaying)
            {
                source.Stop(); // 비활성화 상태에서 재생 중이면 정지
                source.enabled = false; // 비활성화 상태로 변경
            }
        }
    }

    // 게임 시작 시 저장된 상태를 불러오기
    private void LoadAudioSettings()
    {
        BGMSwitch = PlayerPrefs.GetInt("BGMSwitch", 1) == 1; // 기본값은 true
        SoundEffectSwitch = PlayerPrefs.GetInt("SoundEffectSwitch", 1) == 1; // 기본값은 true
    }

    // 상태가 변경될 때 저장하기
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetInt("BGMSwitch", BGMSwitch ? 1 : 0);
        PlayerPrefs.SetInt("SoundEffectSwitch", SoundEffectSwitch ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 새로운 메서드 추가
    public void SoundEffectPlay(int index)
    {
        if (index >= 0 && index < SoundEffect.Length)
        {
            SoundEffect[index].Play();
        }
    }

    // BGMSwitch 또는 SoundEffectSwitch가 변경되었을 때 호출하세요.
    public void OnAudioSettingChanged()
    {
        UpdateAudioSources(); // 음향 설정 업데이트
    }
}
