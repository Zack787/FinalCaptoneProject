using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] AudioMixerSnapshot _patrolSnapShot, _combatSnapShot;
    [SerializeField] AudioClip[] _patrolMusic, _combatMusic;
    [SerializeField] AudioSource _patrolAudioSource, _combatAudioSource;

    int _patrolMusicIndex, _combatMusicIndex;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCombatMusic()
    {
        _combatAudioSource.Stop();
        _combatAudioSource.clip = _combatMusic[_combatMusicIndex];
        _combatAudioSource.Play();
        _combatSnapShot.TransitionTo(1f);
        _combatMusicIndex = (_combatMusicIndex + 1) % _combatMusic.Length;

    }

    public void PlayPatrolMusic()
    {
        _patrolAudioSource.Stop();
        _patrolAudioSource.clip = _patrolMusic[_patrolMusicIndex];
        _patrolAudioSource.Play();
        _patrolSnapShot.TransitionTo(1f);
        _patrolMusicIndex = (_patrolMusicIndex + 1) % _patrolMusic.Length;

    }
}
