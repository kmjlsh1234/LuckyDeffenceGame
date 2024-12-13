using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : SingletonBase<SoundManager>
{
    private AudioSource _sfxSource;
    private AudioSource _bgmSource;

    public Dictionary<string, AudioClip> _sfxDic = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> _bgmDic = new Dictionary<string, AudioClip>();

    public void Init()
    {
        if(_sfxSource == null)
        {
            _sfxSource = this.gameObject.AddComponent<AudioSource>();
            _sfxSource.loop = false;
        }

        if(_bgmSource == null)
        {
            _bgmSource = this.gameObject.AddComponent<AudioSource>();
            _bgmSource.playOnAwake = false;
            _bgmSource.loop = true;
            _bgmSource.volume = 0.1f;
        }

        AudioClip[] _sfxClips = Resources.LoadAll<AudioClip>(Constant.SFX_FILE_PATH);
        foreach(AudioClip clip in _sfxClips)
        {
            _sfxDic.Add(clip.name, clip);
        }
        
        AudioClip[] _bgmClips = Resources.LoadAll<AudioClip>(Constant.BGM_FILE_PATH);
        foreach (AudioClip clip in _bgmClips)
        {
            _bgmDic.Add(clip.name, clip);
        }
    }

    public void PlaySFX(string key)
    {
        var targetClip = GetClip(key, SoundType.SFX);
        _sfxSource.PlayOneShot(targetClip);
    }

    public void PlayBGM(string key)
    {
        _bgmSource.clip = GetClip(key, SoundType.BGM);
        _bgmSource.Play();
    }

    public AudioClip GetClip(string key, SoundType soundType = SoundType.SFX)
    {
        AudioClip clip = null;
        switch (soundType)
        {
            case SoundType.SFX:
                if (_sfxDic.TryGetValue(key, out clip))
                {
                    return clip;
                }
                break;
            case SoundType.BGM:
                if (_bgmDic.TryGetValue(key, out clip))
                {
                    return clip;
                }
                break;
        }
        Debug.LogError($"{key} AudioClip Not Exist!");
        return null;
    }
}
