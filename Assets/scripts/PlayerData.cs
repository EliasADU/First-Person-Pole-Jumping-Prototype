using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Level { get; set; }
    public int Health { get; set; }
    public float[] Position { get; set; }

    public static PlayerData FromPlayer(Player player)
    {
        return new PlayerData
        {
            Level = player.level,
            Health = player.health,

            Position = new float[]
            {
                player.transform.position.x,
                player.transform.position.y,
                player.transform.position.z
            }
        };
    }
}
