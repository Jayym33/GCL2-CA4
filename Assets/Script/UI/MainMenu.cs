using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private RespawnManager respawnManager;

    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

   public void QuitGame()
    {
        Application.Quit();
    }

    public void Credit()
    { // Lock cursor during gameplay
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        SceneManager.LoadScene("Credits");
    }

    public void Instruction()
    {
        // Lock cursor during gameplay
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        SceneManager.LoadScene("Instruction");
    }

    public void Back()
    {
        // Lock cursor during gameplay
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }

}
