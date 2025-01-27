using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunishBlocker : MonoBehaviour
{    
    [SerializeField] Animator anim;
    bool stretch;
    float timeStretching = 0;
    [SerializeField] float blockerDurantion = 3;
    float introAnimationLenght = 0.74f;
    float timer = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        anim.SetTrigger("salpicar");
        float stretchSpeed = 1/(blockerDurantion - introAnimationLenght);
        anim.SetFloat("stretchMultiplier",stretchSpeed);
    }
    
}
