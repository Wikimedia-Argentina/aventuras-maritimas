using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void CrashAnim()
    {
        animator.SetTrigger("choque");
    }
}
