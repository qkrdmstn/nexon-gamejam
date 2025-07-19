using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    STAGE,
}

public enum SFX
{
    COIN,
    HIT,
    PARRY,
}

public class SoundManager : MonoBehaviour
{
    #region private variable

    private static SoundManager _instance;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private List<AudioClip> bgms;

    #endregion // private variable

    #region properties

    public static SoundManager Instance { get { return _instance; } }

    #endregion // properties

    #region mono funcs

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion // mono funcs

    #region public funcs

    public void PlaySFX(string clipName)
    {
        // Ensure the SFX folder exists within the Resources folder.
        AudioClip clip = Resources.Load<AudioClip>($"SFX/{clipName}");
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{clipName}' not found in Resources/SFX/");
        }
    }

    public void PlaySFX(SFX type)
    {
        // Ensure the SFX folder exists within the Resources folder.
        string clipName = clips[(int)type].name;
        AudioClip clip = Resources.Load<AudioClip>($"SFX/{clipName}");
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{clipName}' not found in Resources/SFX/");
        }
    }

    public void PlayBGM(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>($"BGM/{clipName}");
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{clipName}' not found in Resources/BGM/");
        }
    }

    public void PlayBGM(BGM type)
    {
        string clipName = bgms[(int)type].name;
        AudioClip clip = Resources.Load<AudioClip>($"BGM/{clipName}");
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{clipName}' not found in Resources/BGM/");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    #endregion // public funcs
}