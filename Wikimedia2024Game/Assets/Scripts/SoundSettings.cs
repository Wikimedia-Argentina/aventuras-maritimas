using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviourWithContext
{
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider VoiceSlider;

    [SerializeField] private GameObject MusicMutedIcon;
    [SerializeField] private GameObject SFXMutedIcon;
    [SerializeField] private GameObject VoiceMutedIcon;


    public void Show()
    {
        MusicSlider.value = MySoundManager.MusicVolume;
        SFXSlider.value = MySoundManager.SFXVolume;
        VoiceSlider.value = MySoundManager.VoiceVolume;

        MusicMutedIcon.SetActive(MySoundManager.MusicVolume == 0);
        SFXMutedIcon.SetActive(MySoundManager.SFXVolume == 0);
        VoiceMutedIcon.SetActive(MySoundManager.VoiceVolume == 0);

        MusicSlider.onValueChanged.AddListener(OnMusicSliderValueChange);
        SFXSlider.onValueChanged.AddListener(OnSFXSliderValueChange);
        VoiceSlider.onValueChanged.AddListener(OnVoiceSliderValueChange);
    }

    public void Hide()
    {
        MusicSlider.onValueChanged.RemoveAllListeners();
        SFXSlider.onValueChanged.RemoveAllListeners();
        VoiceSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnMusicSliderValueChange(float newValue)
    {
        MySoundManager.ChangeMusicVolume(MusicSlider.value);
        MusicMutedIcon.SetActive(MySoundManager.MusicVolume == 0);
    }

    private void OnSFXSliderValueChange(float newValue)
    {
        MySoundManager.ChangeSFXVolume(SFXSlider.value);
        SFXMutedIcon.SetActive(MySoundManager.SFXVolume == 0);
    }

    private void OnVoiceSliderValueChange(float newValue)
    {
        MySoundManager.ChangeVoiceVolume(VoiceSlider.value);
        VoiceMutedIcon.SetActive(MySoundManager.VoiceVolume == 0);
    }

    public void OnMusicIconClick()
    {
        if(MySoundManager.MusicVolume == 0)
        {
            MySoundManager.ChangeMusicVolume(0.5f);
        }
        else
        {
            MySoundManager.ChangeMusicVolume(0);
        }

        MusicSlider.value = MySoundManager.MusicVolume;
        MusicMutedIcon.SetActive(MySoundManager.MusicVolume == 0);
    }

    public void OnSFXIconClick()
    {
        if (MySoundManager.SFXVolume == 0)
        {
            MySoundManager.ChangeSFXVolume(0.5f);
        }
        else
        {
            MySoundManager.ChangeSFXVolume(0);
        }

        SFXSlider.value = MySoundManager.SFXVolume;
        SFXMutedIcon.SetActive(MySoundManager.SFXVolume == 0);
    }

    public void OnVoiceIconClick()
    {
        if (MySoundManager.VoiceVolume == 0)
        {
            MySoundManager.ChangeVoiceVolume(0.5f);
        }
        else
        {
            MySoundManager.ChangeVoiceVolume(0);
        }

        VoiceSlider.value = MySoundManager.VoiceVolume;
        VoiceMutedIcon.SetActive(MySoundManager.VoiceVolume == 0);
    }

}
