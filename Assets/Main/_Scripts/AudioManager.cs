using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip[] chopsSfx;
    [SerializeField] private AudioClip loseSfx;

    private void Awake()
    {
        // Asegurar que el AudioManager sea un singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
    }

    #region Volumen
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Convierte a escala logarítmica
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // Convierte a escala logarítmica
    }
    #endregion

    #region Reproducción de Sonido
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayChopSFX()
    {
        PlaySFX(chopsSfx[Random.Range(0, chopsSfx.Length)]);
    }

    public void PlayLoseSFX()
    {
        PlaySFX(loseSfx);
    }
    #endregion
}
