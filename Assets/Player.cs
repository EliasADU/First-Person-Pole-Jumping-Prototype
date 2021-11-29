using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Player : MonoBehaviour
{
    public int level = 1; // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public int health = 100;

    public void SavePlayer()
    {
        Save.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = new PlayerData();
        data = Save.LoadPlayer();
        level = data.Level;
        
        //Time.timeScale = 1f;
        this.transform.position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
        LoadingNextLevel(level);
        health = data.Health;

        Thread.Sleep(1000);
        this.transform.position = new Vector3(data.Position[0],data.Position[1],data.Position[2]);
    }
    public void Load(int x)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(x);
        while (!asyncLoad.isDone)
        {
            break;
        }
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
