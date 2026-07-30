using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    //TODO pool audio source
    [SerializeField] private SoundLibrarySO library;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] [Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField] [Range(0f, 1f)] private float sfxVolume = 1f;

    private ENUM_SOUND currentBgm = ENUM_SOUND.None;

    public void PlayBgm(ENUM_SOUND sound)
    {
        if (bgmSource == null)
        {
            Debug.LogWarning("BGM");
            return;
        }

        SoundEntry entry = GetEntry(sound);
        if (entry == null)
        {
            return;
        }

        currentBgm = sound;
        bgmSource.clip = entry.clip;
        bgmSource.loop = entry.loop;
        bgmSource.pitch = entry.pitch;
        bgmSource.volume = entry.volume * musicVolume;
        bgmSource.Play();
    }

    public void StopBgm()
    {
        currentBgm = ENUM_SOUND.None;
        if (bgmSource == null)
        {
            return;
        }

        bgmSource.Stop();
    }
    public void PlaySfx(ENUM_SOUND sound)
    {
        if (sfxSource == null)
        {
            Debug.LogWarning("SFX");
            return;
        }

        SoundEntry entry = GetEntry(sound);
        if (entry == null)
        {
            return;
        }

        sfxSource.pitch = entry.pitch;
        sfxSource.PlayOneShot(entry.clip, entry.volume * sfxVolume);
    }
    private SoundEntry GetEntry(ENUM_SOUND sound)
    {
        if (library == null)
        {
            Debug.LogWarning("Lib");
            return null;
        }

        SoundEntry entry = library.GetSound(sound);
        return entry;
    }
}
