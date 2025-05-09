using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSourceMainTheme;
    [SerializeField] private AudioSource selectionSoundUiAudioSource;
    [SerializeField] private AudioClip selectionSoundClip; 
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    void Start()
    {
        Load();
        audioSourceMainTheme.loop = true;
        audioSourceMainTheme.Play();
    }

    public void PlaySelectionSound()
    {
        if (selectionSoundUiAudioSource != null && selectionSoundClip != null)
        {
            selectionSoundUiAudioSource.PlayOneShot(selectionSoundClip);
        }
    }

    public void SetMasterVolume(float volume)
    {
        float volumeInDb = Mathf.Log10(Mathf.Clamp(volume / 10f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", volumeInDb);
        PlayerPrefs.SetFloat("masterVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float volume)
    {
        float volumeInDb = Mathf.Log10(Mathf.Clamp(volume / 10f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", volumeInDb);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        float volumeInDb = Mathf.Log10(Mathf.Clamp(volume / 10f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MusicVolume", volumeInDb);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        // Master Volume
        float savedMasterVolume = PlayerPrefs.GetFloat("masterVolume", 10f);
        float masterDb = Mathf.Log10(Mathf.Clamp(savedMasterVolume / 10f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MasterVolume", masterDb);
        masterVolumeSlider.value = savedMasterVolume;

        // SFX Volume
        float savedSFXVolume = PlayerPrefs.GetFloat("sfxVolume", 10f);
        float sfxDb = Mathf.Log10(Mathf.Clamp(savedSFXVolume / 10f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", sfxDb);
        sfxVolumeSlider.value = savedSFXVolume;

        // Music Volume
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 10f);
        float volumeInDb = Mathf.Log10(Mathf.Clamp(savedMusicVolume / 10f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MusicVolume", volumeInDb);
        musicVolumeSlider.value = savedMusicVolume;


    }
}
