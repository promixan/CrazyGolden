using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public static VolumeController Instance;

    public AudioMixer mixer;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        var generalVolume = PlayerPrefs.GetFloat(GameConstants.Audio.GENERAL_KEY, 1f);
        var musicVolume = PlayerPrefs.GetFloat(GameConstants.Audio.MUSIC_KEY, 1f);
        var sfxVolume = PlayerPrefs.GetFloat(GameConstants.Audio.SFX_KEY, 1f);
        
        SetMasterVolume(generalVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    public void SetMasterVolume(float sliderValue)
    {
        mixer.SetFloat("MasterVol", CalculateVolumeInDb(sliderValue));
        PlayerPrefs.SetFloat(GameConstants.Audio.GENERAL_KEY, sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVol", CalculateVolumeInDb(sliderValue));
        PlayerPrefs.SetFloat(GameConstants.Audio.MUSIC_KEY, sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFXVol", CalculateVolumeInDb(sliderValue));
        PlayerPrefs.SetFloat(GameConstants.Audio.SFX_KEY, sliderValue);
    }

    private float CalculateVolumeInDb(float sliderValue)
    {
        return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20f;
    }
}
