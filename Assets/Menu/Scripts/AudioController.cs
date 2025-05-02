using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSourceMainTheme;
    [SerializeField] private AudioSource selectionSoundUiAudioSource;
    [SerializeField] private AudioClip selectionSoundClip; 
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

    public void SetMusicVolume(float volume)
    {
        audioSourceMainTheme.volume = volume;
        Save(volume);
    }

    private void Load()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        audioSourceMainTheme.volume = savedMusicVolume;
        musicVolumeSlider.value = savedMusicVolume; 
    }

    private void Save(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }
}
