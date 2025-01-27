using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCutscenes02 : EventsCutscenes
{
    [SerializeField] Animator animator;
    [SerializeField] banner TextBubble;
    [SerializeField] Animator transicionFade;
    [SerializeField] AudioClip sfxCalmSea;
    [SerializeField] AudioClip vozFinal;
    [SerializeField] AudioClip text;

    private void Awake()
    {
        OpenFade();
    }
    private void Start()
    {
        PlayMusic(sfxCalmSea);
    }
    public void OpenFade()
    {
        transicionFade.SetTrigger("open");
    }
    public void FadeToBlack()
    {
        transicionFade.SetTrigger("close");
    }
    public override void DialogoTerminado()
    {
        animator.SetInteger("estado", animator.GetInteger("estado") + 1);
        StopAllSounds(vozFinal);
    }
    public void Saltear()
    {
        animator.SetInteger("estado", 10);
    }
    public void PonerPrimerTextBubble()
    {
        PlaySFX(text);
        PlayVoice(vozFinal);
        TextBubble.Mostrar();
    }
}
