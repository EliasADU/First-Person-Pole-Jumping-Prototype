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
        Data data = Save.LoadPlayer();
        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
