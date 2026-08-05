using UnityEngine;
using System;
public enum ENUM_SOUND
{
    None = 0,

    MenuBgm = 1,
    GameplayBgm = 2,

    PickBrick = 3,
    StairStep = 4,
    Win = 5,
    Fail = 6,

    ButtonClick = 7,
    PopupOpen = 8,
    ReachStage = 9,
}


[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Game/Sound Library")]
public class SoundLibrarySO : ScriptableObject
{
    [SerializeField] private SoundEntry[] sounds;

    public SoundEntry GetSound(ENUM_SOUND sound)
    {
        return sounds[(int)sound];
    }
}

[Serializable]
public class SoundEntry
{
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-3f, 3f)] public float pitch = 1f;
    public bool loop = false;
}
