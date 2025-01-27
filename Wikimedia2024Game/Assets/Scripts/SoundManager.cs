using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private List<AudioSource> audioSource_Active = new List<AudioSource>();
    private List<AudioSource> audioSource_Pool = new List<AudioSource>();

    private AudioSource musicAudioSource;

    public float VoiceVolume { get; private set; }
    public float SFXVolume { get; private set; }
    public float MusicVolume { get; private set; }

    private const string Key_VoiceVolume = "voice_volume";
    private const string Key_SFXVolume = "sfx_volume";
    private const string Key_MusicVolume = "music_volume";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(Key_VoiceVolume))
            VoiceVolume = PlayerPrefs.GetFloat(Key_VoiceVolume);
        else
            VoiceVolume = 1;

        if (PlayerPrefs.HasKey(Key_SFXVolume))
            SFXVolume = PlayerPrefs.GetFloat(Key_SFXVolume);
        else
            SFXVolume = 1;

        if (PlayerPrefs.HasKey(Key_MusicVolume))
            MusicVolume = PlayerPrefs.GetFloat(Key_MusicVolume);
        else
            MusicVolume = 1;
    }

    public void ChangeVoiceVolume(float volume)
    {
        VoiceVolume = volume;
        PlayerPrefs.SetFloat(Key_VoiceVolume, volume);
        PlayerPrefs.Save();
    }

    public void ChangeSFXVolume(float volume)
    {
        SFXVolume = volume;
        PlayerPrefs.SetFloat(Key_SFXVolume, volume);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float volume)
    {
        MusicVolume = volume;
        if(musicAudioSource != null)
            musicAudioSource.volume = volume;
   
        PlayerPrefs.SetFloat(Key_MusicVolume, volume);
        PlayerPrefs.Save();
    }

    public void PlaySfxSound(string clipPath, bool stopAllOtherSounds = false)
    {
        var audioClip = Resources.Load<AudioClip>(clipPath);
        PlaySfxSound(audioClip, stopAllOtherSounds);
    }

    public void PlayMusicLoop(string clipPath, bool loop = true)
    {
        var audioClip = Resources.Load<AudioClip>(clipPath);
        PlayMusicLoop(audioClip, loop);
    }

    public void PlayVoiceSound(string clipPath, bool stopAllOtherSounds = false)
    {
        var audioClip = Resources.Load<AudioClip>(clipPath);
        PlayVoiceSound(audioClip, stopAllOtherSounds);
    }

    public void PlayVoiceSound(AudioClip clip, bool stopAllOtherSounds = false)
    {
        if (stopAllOtherSounds)
        {
            StopAllSounds();
        }

        var audioSource = GetAudioSource();
        audioSource.clip = clip;
        audioSource.volume = VoiceVolume;
        audioSource.loop = false;
        audioSource.Play();
        StartCoroutine(DeactivateAudioSource(audioSource, clip.length));
    }

    public void PlaySfxSound(AudioClip clip, bool stopAllOtherSounds = false)
    {
        if(stopAllOtherSounds)
        {
            StopAllSounds();
        }

        var audioSource = GetAudioSource();
        audioSource.clip = clip;
        audioSource.volume = SFXVolume;
        audioSource.loop = false;
        audioSource.Play();
        StartCoroutine(DeactivateAudioSource(audioSource, clip.length));
    }

    public void PlayMusicLoop(AudioClip clip, bool loop = true)
    {
        if(musicAudioSource == null)
            musicAudioSource = Instantiate(Resources.Load<AudioSource>("Prefabs/GameBase/ReusableAudioSource"), transform);

        if (musicAudioSource.clip != clip)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.volume = MusicVolume;
            musicAudioSource.loop = loop;
            musicAudioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicAudioSource != null)
            musicAudioSource.Pause();
    }

    private AudioSource GetAudioSource()
    {
        AudioSource audioSource;

        if(audioSource_Pool.Count > 0)
        {
            audioSource = audioSource_Pool[0];
            audioSource_Pool.RemoveAt(0);
            audioSource.gameObject.SetActive(true);
        }
        else
        {
            audioSource = Instantiate(Resources.Load<AudioSource>("Prefabs/GameBase/ReusableAudioSource"), transform);
        }

        audioSource_Active.Add(audioSource);
        return audioSource;
    }

    private IEnumerator DeactivateAudioSource(AudioSource audioSource, float clipDurarion)
    {
        yield return new WaitForSeconds(clipDurarion + 1);
        audioSource.gameObject.SetActive(false);
        audioSource_Active.Remove(audioSource);
        audioSource_Pool.Add(audioSource);
    }

    public void StopAllSounds()
    {
        StopAllCoroutines();

        foreach(var audioSource in audioSource_Active)
        {
            audioSource.Stop();
            audioSource.gameObject.SetActive(false);
            audioSource_Pool.Add(audioSource);
        }

        audioSource_Active = new List<AudioSource>();
    }

    public void PauseAll()
    {
        StopAllCoroutines();

        foreach (var audioSource in audioSource_Active)
        {
            audioSource.Pause();
        }

        if(musicAudioSource !=null)
            musicAudioSource.Pause();
    }

    public void ResumeAll()
    {
        StopAllCoroutines();

        foreach (var audioSource in audioSource_Active)
        {
            audioSource.Play();
            StartCoroutine(DeactivateAudioSource(audioSource, audioSource.clip.length));
        }

        if (musicAudioSource != null)
            musicAudioSource.Play();
    }
}
