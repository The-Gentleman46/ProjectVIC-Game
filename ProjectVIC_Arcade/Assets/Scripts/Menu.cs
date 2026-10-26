using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
    
    public void startSnake() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Snake");
    }

    public void Quit() { 
        Application.Quit();
    }
}
