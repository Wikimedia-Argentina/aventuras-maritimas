using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviourWithContext
{
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        MySoundManager.PlaySfxSound(audioClip);
    }
}
