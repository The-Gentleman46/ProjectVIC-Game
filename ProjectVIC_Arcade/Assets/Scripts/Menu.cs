using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void startSnake() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Snake");
    }

    public void startFrogger()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frogger");
    }
}
