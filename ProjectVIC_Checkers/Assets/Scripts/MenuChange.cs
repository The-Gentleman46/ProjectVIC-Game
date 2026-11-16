using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("CheckerBoard");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
