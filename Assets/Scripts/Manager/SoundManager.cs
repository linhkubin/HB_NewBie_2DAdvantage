using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FxType
{
    Run = 0,
    Jump = 1,
    Coin = 2,
    Crash = 3,
    Victory = 4,
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioClip[] audioClips;
    AudioSource[] audios = new AudioSource[Enum.GetNames(typeof(FxType)).Length];
    
    public void Play(FxType type)
    {
        if (audios[(int)type] == null)
        {
            audios[(int)type] = new GameObject(type.ToString()).AddComponent<AudioSource>();
            audios[(int)type].transform.SetParent(transform, false);
            audios[(int)type].loop = false;
            audios[(int)type].playOnAwake = false;
            audios[(int)type].clip = audioClips[(int)type];
        }

        audios[(int)type].Play();
    }
}
