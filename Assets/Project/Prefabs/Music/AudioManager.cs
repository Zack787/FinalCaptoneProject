/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSound;
    [SerializeField] private AudioSource sfxSound;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";

    private void Start()
    {
        // Đặt giá trị Slider theo giá trị hiện tại của âm lượng
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAM, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_PARAM, 0.5f);
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;

        // Cập nhật âm lượng ban đầu
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

        // Thêm sự kiện lắng nghe cho các Slider
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat(MUSIC_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
    }

    private void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat(SFX_VOLUME_PARAM, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFX_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
    }
}
*/