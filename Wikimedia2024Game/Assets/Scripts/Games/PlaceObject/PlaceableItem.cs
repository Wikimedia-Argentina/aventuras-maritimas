using System;
using System.Collections;
using UnityEngine;

public class PlaceableItem : MonoBehaviour
{
    [SerializeField] private Camera worldCam;
    [SerializeField] private Collider2D collider;
    [SerializeField] private GameObject WrongStroke;

    public string Id;

    private ItemHolder isOverHolder = null;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Transform initialParent;
    private Vector3 initialScale;

    private void Awake()
    {
        WrongStroke.SetActive(false);
    }

    internal void SetStartingPosition(Vector3 position)
    {
        initialPosition = position;
        initialParent = transform.parent;
        initialScale = transform.localScale;

        transform.position = position;
    }


    private void OnMouseDrag()
    {
        if (isOverHolder != null && !isOverHolder.IsInEnabledStage)
            return;

        isDragging = true;

        if (isOverHolder != null)
            isOverHolder.SetIsUsed(false);

        transform.position = Utils.UIToWorldPoint(Input.mousePosition, worldCam);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "placeablesBase")
        {
            if (isOverHolder != null)
            {
                isOverHolder.SetIsUsed(false);
                isOverHolder = null;
            }
        }
        if (collision.tag == "itemHolder")
        {
            //Debug.Log("Tigger enter: " + collision.name);
            var maybeIsOverHolder = collision.GetComponent<ItemHolder>();
            if (maybeIsOverHolder.IsInEnabledStage)
            {
                if (isOverHolder != null)
                {
                    isOverHolder.SetIsUsed(false);
                }
                isOverHolder = maybeIsOverHolder;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "itemHolder")
        {
            if (isOverHolder != null && isOverHolder == collision.GetComponent<ItemHolder>())
            {
                isOverHolder.SetIsUsed(false);
                isOverHolder = null;
            }
        }
    }

    private void Update()
    {
        if(isDragging)
        {
            if(Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if(isOverHolder != null)
                {
                    transform.parent = isOverHolder.transform;
                    transform.localPosition = Vector3.zero;
                    bool isRight = isOverHolder.SetIsUsed(true, Id);
                    if(isRight)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        StartCoroutine(GoBackToStartInSeconds(1));
                    }
                }
                else
                {
                    transform.parent = initialParent;
                    transform.position = initialPosition;
                    transform.localScale = initialScale;
                }
            }
        }
    }

    private IEnumerator GoBackToStartInSeconds(float seconds)
    {
        WrongStroke.SetActive(true);
        collider.enabled = false;
        yield return new WaitForSeconds(seconds);
        transform.parent = initialParent;
        transform.position = initialPosition;
        transform.localScale = initialScale;
        if (isOverHolder != null)
        {
            isOverHolder.SetIsUsed(false);
            isOverHolder = null;
        }
        collider.enabled = true;
        WrongStroke.SetActive(false);
    }
}
