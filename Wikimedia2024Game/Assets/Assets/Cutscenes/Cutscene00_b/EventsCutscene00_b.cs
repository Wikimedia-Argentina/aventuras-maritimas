using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCutscene00_b : EventsCutscenes
{
    [SerializeField] Animator animator;
    [SerializeField] banner banner1;
    [SerializeField] AudioClip banner1Clip;
    [SerializeField] banner banner2;
    [SerializeField] AudioClip banner2Clip;
    [SerializeField] banner TextBubble1;
    [SerializeField] AudioClip TextBubble1Clip;
    [SerializeField] banner TextBubble2;
    [SerializeField] AudioClip TextBubble2Clip;
    [SerializeField] Animator transicionCirculo;
    [SerializeField] Animator transicionFade;
    [SerializeField] AudioClip text;
    [SerializeField] AudioClip musicSlpash;

    private void Awake()
    {
        AbrirCirculo();
    }
    private void Start()
    {
        PlayMusic(musicSlpash);
    }
    public void PonerPrimerTextBubble()
    {
        StopAllSounds(banner2Clip);
        PlaySFX(text);
        TextBubble1.Mostrar();
        PlayVoice(TextBubble1Clip);
        
    }
    public void PonerSegundoTextBubble()
    {
        StopAllSounds(TextBubble1Clip);
        PlaySFX(text);
        TextBubble2.Mostrar();
        PlayVoice(TextBubble2Clip);
        
    }
    public void PonerPrimerBanner()
    {
        PlaySFX(text);
        banner1.Mostrar();
        PlayVoice(banner1Clip);

    }
    public void PonerSegundoBanner()
    {
        StopAllSounds(banner1Clip);
        PlaySFX(text);
        banner2.Mostrar();
        PlayVoice(banner2Clip);
        

    }
    public void AbrirCirculo()
    {
        transicionCirculo.SetTrigger("open");
    }
    public void FadeToBlack()
    {
        transicionFade.SetTrigger("close");
        StopAllSounds(TextBubble2Clip);
    }
    public override void DialogoTerminado()
    {
        animator.SetInteger("estado", animator.GetInteger("estado") + 1);
    }
    public void Saltear()
    {
        animator.SetInteger("estado", 10);
    }
}
