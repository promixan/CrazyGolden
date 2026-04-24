using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Sound Clips")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float volume = 1f;

    private static AudioSource _audioSource;

    private void Awake()
    {
        if (_audioSource == null)
        {
            _audioSource = FindAnyObjectByType<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogWarning("ButtonSounds: No AudioSource found in the scene!");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(clickSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (_audioSource != null && clip != null)
            _audioSource.PlayOneShot(clip, volume);
    }
}
