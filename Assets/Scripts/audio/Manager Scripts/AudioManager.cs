using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [SerializeField] private int _sfxCount = 5;
    private int _curIndex = 0;

    [SerializeField] private AudioSource[] _sfxArray;
    private AudioSource _bgm;

    /// <summary>
    /// Awake is called when this script gets activated
    /// 
    /// </summary>
    private void Awake()
    {
        if (AudioManager.audioManager == null)
        {
            AudioManager.audioManager = this;
        }
        else if (AudioManager.audioManager != this)
        {
            Destroy(this);
        }

        Initialize();
    }

    private void Initialize()
    {
        _sfxArray = new AudioSource[_sfxCount];

        for (int i = 0; i < _sfxArray.Length; i++)
        {
            _sfxArray[i] = gameObject.AddComponent<AudioSource>();
        }

        _bgm = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clipToPlay, float pitch)
    {
        _sfxArray[_curIndex].clip = clipToPlay;
        _sfxArray[_curIndex].pitch = pitch;
        _sfxArray[_curIndex].Play();

        _curIndex++;

        if (_curIndex > _sfxCount - 1)
        {
            _curIndex = 0;
        }
    }

    public void PlayBGM(AudioClip clipToPlay, float transitionTime, bool isLooping)
    {
        StartCoroutine(CrossFadeBGM(clipToPlay, transitionTime, isLooping));
    }

    private IEnumerator CrossFadeBGM(AudioClip clipToPlay, float transitionTime, bool isLooping)
    {
        AudioSource newBGM = gameObject.AddComponent<AudioSource>();
        newBGM.clip = clipToPlay;
        newBGM.loop = isLooping;
        newBGM.volume = 0;

        newBGM.Play();

        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            _bgm.volume = (1 - (t / transitionTime));
            newBGM.volume = (t / transitionTime);
            yield return null;
        }

        newBGM.volume = 1;
        Destroy(_bgm);
        _bgm = newBGM;
    }
}
