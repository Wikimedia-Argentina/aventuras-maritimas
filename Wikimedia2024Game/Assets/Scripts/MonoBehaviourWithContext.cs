using UnityEngine;

public class MonoBehaviourWithContext : MonoBehaviour
{
    protected SoundManager MySoundManager { get { return Context.Instance.SoundManager; } }
    protected PlayerStatus MyPlayerStatus { get { return Context.Instance.PlayerStatus; } }
    protected Localization MyLocalization { get { return Context.Instance.Localization; } }
}

