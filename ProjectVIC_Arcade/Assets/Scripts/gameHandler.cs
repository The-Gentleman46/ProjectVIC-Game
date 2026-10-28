using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public static gameHandler instance { get; private set; }
 

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    
}
