
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level : Stat
{
    public int lvlUpXpMultiplier = 10;
    public int lvlUpXpNeededMultiplier = 100;
    public int lvlUpStatAmount = 5;
    public Level(StatEnum se) : base (se)
    {
        base.statName = StatEnum.Level;

    }
}
