using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public static gameHandler instance { get; private set; }

    private int Length = 1;

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

    public int getLength()
    {
        return Length;
    }

    public void addLength()
    {
        Length++;


    }
}
