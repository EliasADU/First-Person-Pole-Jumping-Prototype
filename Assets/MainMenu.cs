using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void NewGame()
    {
        SceneManager.LoadScene("testing");
        //any testing scene can be put here
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //plays next scene in file/build settings
    }
    public void LoadGame()
    {
        PlayerData data = new PlayerData();
        data = Save.LoadPlayer();

        //Time.timeScale = 1f;
      
        LoadingNextLevel(data.Level);

        //this.transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
    }
    public void Options()
    {
        //Options
        //settings has its own script and panel
    }
    public void Quit()
    {
        //Quit
        Debug.Log("Quiting Game");
        Application.Quit();
    }
    public IEnumerator LoadingNextLevel(int levelToLoad)
    {
        PlayerData data = new PlayerData();
        data = Save.LoadPlayer();

        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        this.transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
    }
}

