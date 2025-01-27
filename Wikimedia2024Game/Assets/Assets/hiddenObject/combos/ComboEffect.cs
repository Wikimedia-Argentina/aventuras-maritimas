using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboEffect : MonoBehaviour
{
    [SerializeField] Animator[] anims;
    [SerializeField] Canvas parentCanvas;

    public void PlayCombo()
    {
        UpdatePosition();
        anims[0].SetTrigger("combo");
    }
    public void PlaySuperCombo()
    {
        UpdatePosition();
        foreach (Animator anim in anims) 
        {
            anim.SetTrigger("combo");
        }
    }

    public void Start()
    {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out pos);
    }

    void UpdatePosition()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        transform.position = parentCanvas.transform.TransformPoint(movePos);
    }

}
