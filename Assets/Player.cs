using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(level);
        health = data.Health;

        Vector3 position;
        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];
        transform.position = position;
    }
}
