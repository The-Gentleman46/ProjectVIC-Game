using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour
{
    //Loads the game scene when the "Start Game (2 Players needed)" button is clicked
    public void LoadGame()
    {
        SceneManager.LoadScene("CheckerBoard");
    }

    //Loads the main menu scene when the "Return to Menu" button is clicked
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Loads the instructions scene when the "Open Instructions" button is clicked
    public void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    // Quits the application when the "Exit" button is clicked
    public void Quit()
    {
        Application.Quit();
    }
}
