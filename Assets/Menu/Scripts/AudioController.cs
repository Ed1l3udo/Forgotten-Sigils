using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceMainTheme;
    [SerializeField] private Slider volumeSlider;
    void Start()
    {
        Load();
        audioSourceMainTheme.loop = true;
        audioSourceMainTheme.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(float volume)
    {
        audioSourceMainTheme.volume = volume;
        Save(volume);
    }

    private void Load()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
         Debug.Log("Volume carregado: " + savedVolume);
        audioSourceMainTheme.volume = savedVolume;
        volumeSlider.value = savedVolume; 
    }

    private void Save(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }
}
