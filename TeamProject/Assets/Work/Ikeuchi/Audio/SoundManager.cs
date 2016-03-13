using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    AudioSource _bgmSource; // BGM用
    
    [SerializeField]
    AudioSource _seSource; // SE用

    public static SoundManager _instance = null;

    [SerializeField]
    AudioClip[] _bgms;
    [SerializeField]
    AudioClip[] _ses;

    // 音の高さ
    public static float _bgmVolume = 0.5f;
    public static float _seVolume = 0.5f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void BgmPlay(int index)
    {
        _bgmSource.volume = _bgmVolume;
        _bgmSource.clip = _bgms[index];
        _bgmSource.Play();
    }

    public void BgmStop()
    {
        _bgmSource.Stop();
    }

    public void SePlaySingle(int index)
    {
        _seSource.volume = _seVolume;
        _seSource.clip = _ses[index];
        _seSource.Play();
    }

    public void SePlay(int index)
    {      
        _seSource.volume = _seVolume;
        _seSource.PlayOneShot(_ses[index]);
    }

    public void SeStop()
    {
        _seSource.Stop();
    }

    public void ChangeBgmPitch(float pitch)
    {
        if(pitch > 1.0f) { pitch = 1.0f; }
        if(pitch < 0.0f) { pitch = 0.0f; }
        _bgmVolume = pitch;
        _bgmSource.volume = _bgmVolume;
    }

    public void ChangeSePitch(float pitch)
    {
        if (pitch > 1.0f) { pitch = 1.0f; }
        if (pitch < 0.0f) { pitch = 0.0f; }
        _seVolume = pitch;
        _seSource.volume = _seVolume;
    }
}
