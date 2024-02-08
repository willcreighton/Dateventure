using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip hoverSound, clickSound, slightRevealSound, fullRevealSound;
    private AudioSource hoverAudioSource, clickAudioSource, slightRevealAudioSource, fullRevealAudioSource;

    float mouseVolume = 0.4f;
    float cardVolume = 0.7f;

    void Start()
    {
        // Create separate AudioSource components for each sound
        hoverAudioSource = AddAudioSource(hoverSound, mouseVolume);
        clickAudioSource = AddAudioSource(clickSound, mouseVolume);
        slightRevealAudioSource = AddAudioSource(slightRevealSound, cardVolume);
        fullRevealAudioSource = AddAudioSource(fullRevealSound, cardVolume);
    }

    public void PlayHoverSound()
    {
        PlaySound(hoverAudioSource, hoverSound);
    }

    public void PlayClickSound()
    {
        PlaySound(clickAudioSource, clickSound);
    }

    public void PlaySlightRevealSound()
    {
        PlaySound(slightRevealAudioSource, slightRevealSound);
    }

    public void PlayFullRevealSound()
    {
        PlaySound(fullRevealAudioSource, fullRevealSound);
    }

    private AudioSource AddAudioSource(AudioClip clip, float volume)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.volume = volume;

        return source;
    }

    private void PlaySound(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null && !source.isPlaying)
        {
            source.Play();
        }
    }
}