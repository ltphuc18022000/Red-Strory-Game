using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerID { get; set; }
    public int gold { get; set; }
    public int level { get; set; }
    public float health { get; set; }
    public int attackDamage { get; set; }
    public float speedRun { get; set; }
    public float jumbHeight { get; set; }

    public PlayerData(int playerID, int gold, int level, float health, int attackDamage, float speedRun, float jumbHeight)
    {
        this.playerID = playerID;
        this.gold = gold;
        this.level = level;
        this.health = health;
        this.attackDamage =attackDamage;
        this.speedRun = speedRun;
        this.jumbHeight = jumbHeight;
    }
}
