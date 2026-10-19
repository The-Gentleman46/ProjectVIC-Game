using UnityEngine;

public class Menu : MonoBehaviour
{
    public void menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    
    public void startSnake() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Snake");
    }

    public void Quit() { 
        Application.Quit();
    }
}
