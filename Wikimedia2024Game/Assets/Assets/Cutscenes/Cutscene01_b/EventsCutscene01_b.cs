using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCutscene01_b : EventsCutscenes
{
    [SerializeField] Animator animator;
    [SerializeField] banner banner1;
    [SerializeField] Animator transicionFade;
    [SerializeField] AudioClip sfxSplash;
    [SerializeField] AudioClip sfxCamlSea;
    [SerializeField] AudioClip dialogo;
    [SerializeField] AudioClip text;
    private void Awake()
    {
        OpenFade();
    }
    private void Start()
    {
        PlayMusic(sfxCamlSea);
    }
    public void PonerPrimerBanner()
    {
        banner1.Mostrar();
        PlayVoice(dialogo);
        PlaySFX(text);
    }
    public void OpenFade()
    {
        transicionFade.SetTrigger("open");
    }
    public override void DialogoTerminado()
    {
        animator.SetInteger("estado", animator.GetInteger("estado") + 1);
        StopAllSounds(dialogo);
    }
    public void Saltear()
    {
        animator.SetInteger("estado", 10);
    }
    public void FadeToBlack()
    {
        
        StopAllSounds(sfxSplash);
        transicionFade.SetTrigger("close");
    }
    public void SplashAudio()
    {
        PlaySFX(sfxSplash);
    }
}
