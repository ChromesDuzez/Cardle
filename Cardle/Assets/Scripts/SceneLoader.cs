/*
* Zach Wilson
* Cardle
* This script manages the changing of scenes throughout the menus
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Cardle Main Menu")
        {
            Time.timeScale = 1.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void LoadScene(string sceneName)
    {
        Debug.Log("SceneLoader");
        Debug.Log("sceneName to load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}