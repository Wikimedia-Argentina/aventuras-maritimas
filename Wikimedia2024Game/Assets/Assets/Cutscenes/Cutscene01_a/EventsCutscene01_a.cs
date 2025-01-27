using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCutscene01_a : EventsCutscenes
{
    [SerializeField] Animator animator;
    [SerializeField] banner TextBubble1;
    [SerializeField] banner TextBubble2;
    [SerializeField] banner TextBubble3;
    [SerializeField] Animator transicionFade;
    [SerializeField] AudioClip sfxSuspense;
    [SerializeField] AudioClip sfxCalm;
    [SerializeField] AudioClip loro_1;
    [SerializeField] AudioClip loro_2;
    [SerializeField] AudioClip loro_3;
    [SerializeField] AudioClip text;
    private void Awake()
    {
        OpenFade();
    }
    private void Start()
    {
        PlayMusic(sfxSuspense);
    }
    public void CambiarMusica()
    {
        //StopAllSounds(sfxSuspense);
        PlayMusic(sfxCalm);
    }
    public void PonerPrimerTextBubble()
    {
        PlaySFX(text);
        TextBubble1.Mostrar();
        PlayVoice(loro_1);
    }
    public void PonerSegundoTextBubble()
    {
        
        StopAllSounds(loro_1);
        PlaySFX(text);
        TextBubble2.Mostrar();
        PlayVoice(loro_2);
        
    }
    public void PonerTercerTextBubble()
    {
        
        StopAllSounds(loro_2);
        PlaySFX(text);
        TextBubble3.Mostrar();
        PlayVoice(loro_3);
        
    }
    public void OpenFade()
    {
        transicionFade.SetTrigger("open");
    }
    public void FadeToBlack()
    {
        StopAllSounds(loro_3);
        transicionFade.SetTrigger("close");
    }
    public void Saltear()
    {
        animator.SetInteger("estado", 10);
    }
    public void EntraLoro()
    {
        animator.SetTrigger("loro_entra");
        CambiarMusica();
    }
    public void LoroASalvo()
    {
        animator.SetInteger("estado", animator.GetInteger("estado") + 1);
    }
    public override void DialogoTerminado()
    {
        animator.SetInteger("estado", animator.GetInteger("estado") + 1);
    }
}
