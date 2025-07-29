using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    private AudioSource sfxSource;
    private AudioSource musicSource;

    [Header("Clips de Efeito")]
    public AudioClip somDash;
    public AudioClip somFogo;
    public AudioClip somForce;
    public AudioClip somVento;
    public AudioClip passoDireita;
    public AudioClip passoEsquerda;

    [Header("Clips do Boss")]
    public AudioClip somSummon;
    public AudioClip somAtaque;

    [Header("Volume")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;

    [Header("Sliders (Opcional)")]
    public Slider sfxSlider;
    public Slider musicSlider;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Cria e configura os dois AudioSources
            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AtualizarVolumes();

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
    }

    void AtualizarVolumes()
    {
        sfxSource.volume = sfxVolume;
        musicSource.volume = musicVolume;
    }

    // Métodos públicos para tocar sons
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Métodos de volume para UI
    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        AtualizarVolumes();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        AtualizarVolumes();
    }

    // Métodos facilitadores
    public void PlaySomFogo() => PlaySFX(somFogo);
    public void PlaySomForce() => PlaySFX(somForce);
    public void PlaySomVento() => PlaySFX(somVento);
    public void PlaySomDash() => PlaySFX(somDash);
    public void PlayPassoDireita() => PlaySFX(passoDireita);
    public void PlayPassoEsquerda() => PlaySFX(passoEsquerda);
    public void PlayBossSummon() => PlaySFX(somSummon);
    public void PlayBossAtaque() => PlaySFX(somAtaque);
}
