using Data;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderVolumes : MonoBehaviour
{
    public GameConstants.Audio.VolumeType sliderType;

    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Start()
    {
        switch (sliderType)
        {
            case GameConstants.Audio.VolumeType.General:
                _slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(GameConstants.Audio.GeneralKey, 1f));
                break;
            case GameConstants.Audio.VolumeType.Music:
                _slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(GameConstants.Audio.MusicKey, 1f));
                break;
            case GameConstants.Audio.VolumeType.Sfx:
                _slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(GameConstants.Audio.SfxKey, 1f));
                break;
        }
    }

    public void OnValueChanged(float value)
    {
        switch (sliderType)
        {
            case GameConstants.Audio.VolumeType.General:
                VolumeController.Instance.SetMasterVolume(value);
                break;
            case GameConstants.Audio.VolumeType.Music:
                VolumeController.Instance.SetMusicVolume(value);
                break;
            case GameConstants.Audio.VolumeType.Sfx:
                VolumeController.Instance.SetSFXVolume(value);
                break;
        }
    }
}
