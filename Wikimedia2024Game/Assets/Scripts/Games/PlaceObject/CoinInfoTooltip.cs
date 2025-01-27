using TMPro;
using UnityEngine;

public class CoinInfoTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text clueTxt;

    public void Show(string clueText, Vector3 position)
    {
        transform.position = position;
        clueTxt.text = clueText;
        
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
