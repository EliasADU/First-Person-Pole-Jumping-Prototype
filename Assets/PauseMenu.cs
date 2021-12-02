using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    //MouseLook player;

    //checks every frame if game is paused or not when escape is pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P))
        {
                Pause();
         
        }
    }

    //resume starts the game again
    public void Resume()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        //player.cameraMovementIsEnabled=true;
        GameIsPaused = false;
    }

    //pause pauses time
    void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        //player.cameraMovementIsEnabled=false;
        GameIsPaused = true;
    }

    //loads the main menu scene
    public void LoadMenu()

    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    //quits out of the game
    public void QuitGame()
    {
        Cursor.visible = true;
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
