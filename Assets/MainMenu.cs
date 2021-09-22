using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        //SceneManager.LoadScene("testing_grace 1")
        //any testing scene can be put here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //plays next scene in file/build settings
    }
    public void LoadGame()
    {
        //SceneManager.LoadScene("testing_grace 1")
        //any testing scene can be put here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //plays next scene in file/build settings
    }
    public void Options()
    {
        //Options
    }
    public void Quit()
    {
        //Quit
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}

