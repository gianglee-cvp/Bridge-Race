using System.Collections.Generic;
using UnityEngine;
public enum AudioSourceType
{
    Music = 0,
    SFX = 1
}
public class SoundManager : Singleton<SoundManager>
{
    //TODO pool audio source
    [SerializeField] private SoundLibrarySO library;
    [SerializeField] private List<AudioSource> listAudio = new List<AudioSource>();
    [SerializeField] [Range(0f, 1f)] private float musicVolume ;
    [SerializeField] [Range(0f, 1f)] private float sfxVolume ;
    protected bool musicOn = true;

    private ENUM_SOUND currentBgm = ENUM_SOUND.None;

    public void OnInit()
    {
        ApplyMusicState(GameManager.Instance.dataManager.GetMusic());
    }
    public void PlayBgm(ENUM_SOUND sound)
    {
        SoundEntry entry = GetEntry(sound);
        if (entry == null)
        {
            return;
        }
        AudioSource bgmSource = listAudio[(int)AudioSourceType.Music];

        currentBgm = sound;
        bgmSource.clip = entry.clip;
        bgmSource.loop = entry.loop;
        bgmSource.pitch = entry.pitch;
        bgmSource.volume = entry.volume * musicVolume;
        bgmSource.mute = !musicOn;
        // bgmSource.volume = 0f;  
        bgmSource.Play();
    }

    public void StopBgm()
    {
        AudioSource bgmSource = listAudio[(int)AudioSourceType.Music];
        currentBgm = ENUM_SOUND.None;
        if (bgmSource == null)
        {
            return;
        }
        bgmSource.Stop();
    }
    public void PlaySfx(ENUM_SOUND sound)
    {
        AudioSource sfxSource = listAudio[(int)AudioSourceType.SFX];
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
    public void SetBool(AudioSourceType audio , bool soundOn)
    {
        AudioSource src = listAudio[(int)audio];
        src.mute = !soundOn;
        if (audio == AudioSourceType.Music)
        {
            musicOn = soundOn;
        }
    }
    public bool SoundButton()
    {
        ApplyMusicState(!musicOn);
        return musicOn;
    }

    private void ApplyMusicState(bool isOn)
    {
        musicOn = isOn;
        SetBool(AudioSourceType.Music, musicOn);
    }
}
