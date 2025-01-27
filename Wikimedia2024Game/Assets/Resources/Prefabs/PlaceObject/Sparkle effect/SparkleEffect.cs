using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleEffect : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void PlaySparkle(Vector3 position)
    {
        UpdatePosition(position);
        animator.SetTrigger("sparkle");
    }

    public void Start()
    {
        
    }

    void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }
}
