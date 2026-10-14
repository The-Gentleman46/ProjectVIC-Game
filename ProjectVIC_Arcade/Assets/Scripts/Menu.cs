using UnityEngine;

public class Menu : MonoBehaviour
{
    public void menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void startCheckers()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Checkers");
    }

    public void startFrogger()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frogger");
    }

    public void startPong()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Pong");
    }
    
    public void startSnake() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Snake");
    }

    
}
