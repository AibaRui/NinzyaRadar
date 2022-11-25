using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("BGM")]
    [Tooltip("BGM�p��AudioClip"), SerializeField]
    AudioClip _bgmAudioClip;
    [Tooltip("�V�[�����Ă΂ꂽ�^�C�~���O�ōĐ�����"), SerializeField]
    bool _playOnAwake = true;
    [Tooltip("BGM��Loop"), SerializeField]
    bool _isLoop = true;
    [Tooltip("BGM�̃{�����[��"), SerializeField, Range(0f, 1f)]
    float _bgmVolume = 1f;

    [Header("SE")]
    [Tooltip("SE�p��AudioClip"), SerializeField]
    AudioClip[] _seAudioClips;
    [Tooltip("SE�p��AudioSource��LifeTime�̕b��"), SerializeField]
    float _seLifeTime = 300f;

    AudioSource _bgmAudioSource;
    Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    private void Start()
    {
        if (_bgmAudioClip)
        {
            _bgmAudioSource = this.AddComponent<AudioSource>();
            _bgmAudioSource.loop = _isLoop;
            _bgmAudioSource.clip = _bgmAudioClip;
            _bgmAudioSource.volume = _bgmVolume;

            if (_playOnAwake)
            {
                _bgmAudioSource.Play();
            }
        }

        foreach (var n in _seAudioClips)
        {
            _clips.Add(n.name, n);
        }
    }

    public void PlayBGM()
    {
        _bgmAudioSource?.Play();
    }

    public void PlayBGM(AudioClip audioClip)
    {
        if (_bgmAudioSource == null)
        {
            _bgmAudioSource = this.AddComponent<AudioSource>();
            _bgmAudioSource.loop = _isLoop;
            _bgmAudioSource.clip = audioClip;
            _bgmAudioSource.volume = _bgmVolume;
            _bgmAudioSource.Play();
        }
    }

    public void PlaySE(AudioClip seClip)
    {
        var temp = this.AddComponent<AudioSource>();
        temp.clip = seClip;
        temp.Play();
        Destroy(temp, _seLifeTime);
    }

    public void PlaySE(string clipname)
    {
        var temp = this.AddComponent<AudioSource>();
        temp.clip = _clips[clipname];
        temp.Play();
        Destroy(temp, _seLifeTime);
    }
}
