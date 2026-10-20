using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public static gameHandler instance { get; private set; }

    [SerializeField] private Vector2 gridsize = new(20, 20);

    private int currentScore = 0;
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

    public int getScore()
    {
        return currentScore;
    }

    public void addScore()
    {
        currentScore++;
    }
}
