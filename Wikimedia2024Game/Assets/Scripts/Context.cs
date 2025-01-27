using UnityEngine;

public class Context : MonoBehaviour
{
    //Public objects that will be needed and shared between all the scenes
    public SoundManager SoundManager;
    public PlayerStatus PlayerStatus;
    public Localization Localization;

    //--------------------------------------------------------------------

    private static Context instance;
    public static Context Instance 
    {
        get 
        {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<Context>("Prefabs/GameBase/Context"));
            }
            return instance;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void Hello()
    {
        //Just a method to wake up the context
    }
}
