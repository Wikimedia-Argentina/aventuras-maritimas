using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCutscene00a : EventsCutscenes
{
    [SerializeField] AudioClip musicIntro;
    [SerializeField] Animator animator;
    [SerializeField] banner banner1;
    [SerializeField] banner banner2;
    [SerializeField] AudioClip banner1Clip;
    [SerializeField] AudioClip banner2Clip;
    [SerializeField] Animator transicionCirculo;
    [SerializeField] Animator transicionFade;
    [SerializeField] AudioClip text;

    private void Awake()
    {
        OpenFade();
        
    }
    private void Start()
    {
        PlayMusic(musicIntro);
    }
    public void PonerPrimerBanner()
    {
        PlaySFX(text);
        banner1.Mostrar();
        PlayVoice(banner1Clip);
        
    }
    public void PonerSegundoBanner()
    {
        PlaySFX(text);
        StopAllSounds(banner1Clip);
        banner2.Mostrar();
        PlayVoice(banner2Clip);
    }
    public void CerrarCirculo()
    {
        StopAllSounds(banner2Clip);
        transicionCirculo.SetTrigger("close");
    }
    public override void DialogoTerminado()
    {
      animator.SetInteger("estado", animator.GetInteger("estado") + 1);
    }
    public void Saltear()
    {
        animator.SetInteger("estado", 10);
    }
    public void OpenFade()
    {
        transicionFade.SetTrigger("open");
    }
}