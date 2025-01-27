using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banner : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float tiempoMaximo = 15f;
    float timer = 0;
    [SerializeField] GameObject button;
    [SerializeField] EventsCutscenes eventsCutscene;
    void Update()
    {
        if (animator.GetBool("visible"))
        {
            timer += Time.deltaTime;
            if(timer > tiempoMaximo)
            {
                Ocultar();
            }
        }
    }

    public void Mostrar()
    {
        animator.SetBool("visible",true);
        timer = 0;
        button.SetActive(true);
    }
    public void Ocultar()
    {
        animator.SetBool("visible", false);
        button.SetActive(false);
        eventsCutscene.DialogoTerminado();
    }
}
