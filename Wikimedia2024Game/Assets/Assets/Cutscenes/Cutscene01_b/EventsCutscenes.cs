using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsCutscenes : MonoBehaviourWithContext
{
    [SerializeField] Cutscene cutsceneScritp;

    private void Awake()
    {
        //Esto es para que ande el Context cuando se le da play a la escena sin venir navegando desde el Home
        Context.Instance.Hello();
    }
    public virtual void DialogoTerminado()
    {

    }
    public void Continuar()
    {
        cutsceneScritp.Continue();
    }

    public void PlaySFX(AudioClip clip)
    {
        MySoundManager.PlaySfxSound(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        MySoundManager.PlayMusicLoop(clip);
    }

    public void PlayVoice(AudioClip clip)
    {
        MySoundManager.PlayVoiceSound(clip);
    }

    public void StopAllSounds(AudioClip clip)
    {
        MySoundManager.StopAllSounds();
    }

}
