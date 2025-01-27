using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punishBlockerPlaceObject : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float blockerDurantion = 3;
    float closingAnimationLenght = 1.12f;
    float openingAnimationLength = 0.57f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        anim.SetTrigger("cerrar");
        float standbySpeed = 1 / (blockerDurantion - closingAnimationLenght - openingAnimationLength);
        anim.SetFloat("standbyMultiplier", standbySpeed);
    }
}
