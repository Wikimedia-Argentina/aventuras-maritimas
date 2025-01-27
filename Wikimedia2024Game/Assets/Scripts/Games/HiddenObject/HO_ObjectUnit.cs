using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HO_ObjectUnit : MonoBehaviourWithContext
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 netPosition;

    public UnityEvent<HO_ObjectUnit> OnClicked;
    public bool UnitFound;

    public void OnClick()
    {
        OnClicked.Invoke(this);
    }

    public void AnimateError()
    {
        MySoundManager.PlaySfxSound("Sound/HiddenObject/SFXWrongObject");
        animator.Play("ObjUnitAnimError");
    }

    public void AnimateRight(bool isMainObject)
    {
        UnitFound = true;

        if (isMainObject)
            MySoundManager.PlaySfxSound("Sound/HiddenObject/SFXHauntedCoin");
        else
            MySoundManager.PlaySfxSound("Sound/HiddenObject/SFXFindObject");

        animator.Play("ObjUnitAnimRight");
    }

    public void FinishAnimateRight()
    {
        StartCoroutine(TravelToPointAndDissapear());
    }

    public float speed = 80f;
    private IEnumerator TravelToPointAndDissapear()
    {
        //Travel
        Vector3 currentPosition = transform.localPosition;
        float distanceX = netPosition.x - currentPosition.x;
        float distanceY = netPosition.y - currentPosition.y;

        while (Math.Abs(distanceX) > .5f || Math.Abs(distanceY) > .5f)
        {
            currentPosition = Vector2.MoveTowards(currentPosition, netPosition, speed * Time.deltaTime);

            transform.localPosition = currentPosition;

            distanceX = netPosition.x - currentPosition.x;
            distanceY = netPosition.y - currentPosition.y;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);

        //Dissapear
        animator.Play("ObjUnitDissapear");
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }

}
