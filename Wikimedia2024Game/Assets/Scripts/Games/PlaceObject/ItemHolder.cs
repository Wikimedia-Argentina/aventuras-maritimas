using UnityEngine;
using UnityEngine.Events;

public class ItemHolder : MonoBehaviourWithContext
{
    [SerializeField] private Collider2D coll;

    [SerializeField] public bool IsInEnabledStage;
    [SerializeField] private SpriteRenderer image;

    [SerializeField] private AudioClip rightPlacementAudioClip;
    [SerializeField] private AudioClip wrongPlacementAudioClip;

    public string Id;

    public UnityEvent<string, bool> OnStatusChange;
    public UnityEvent<string, Vector3> OnMouseOver;
    public UnityEvent<string, Vector3> OnMouseOut;

    public bool IsRight { get; private set; }
    public bool IsUsed { get; private set; }

    private void OnMouseEnter()
    {
       if (IsInEnabledStage && !IsUsed)
            OnMouseOver.Invoke(Id, transform.position);
    }

    private void OnMouseExit()
    {
        OnMouseOut.Invoke(Id, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnMouseEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnMouseExit();
    }

    public bool SetIsUsed(bool value, string anId = "")
    {
        IsUsed = value;
        coll.enabled = !IsUsed;

        IsRight = anId == Id;

        if (value)
        {
            OnMouseExit();

            if (IsRight) 
            {
                MySoundManager.PlaySfxSound(rightPlacementAudioClip);
                GameObject.FindObjectOfType<SparkleEffect>().GetComponent<SparkleEffect>().PlaySparkle(transform.position);
            }
            else
                MySoundManager.PlaySfxSound(wrongPlacementAudioClip);

            OnStatusChange.Invoke(Id, IsRight);
        }

        if (value && IsRight)
        {
            Deactivate();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Deactivate()
    {
        coll.enabled = false;
        image.enabled = true;
    }

    public void Activate()
    {
        IsUsed = false;
        coll.enabled = true;
        IsRight = false;
        image.enabled = false;
    }
}
