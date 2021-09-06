using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatEnum
{
    Agility,
    Strength,
    Intiligenz,
    Spirit,
    Stammina,
    Armor,
    Damage,
    Level,
    [HideInInspector]NumberOfTypes
}
[System.Serializable]
public class StatList
{
    [HideInInspector] public Stat agility;
    [HideInInspector] public Stat strength;
    [HideInInspector] public Stat intiligenz;
    [HideInInspector] public Stat spirit;
    [HideInInspector] public Stat stammina;
    [HideInInspector] public Stat armor;
    [HideInInspector] public Stat damage;

    [SerializeField] public Level level = new Level(StatEnum.Level);


    public List<Stat> stats = new List<Stat>{ new Stat(StatEnum.Agility), new Stat(StatEnum.Strength), new Stat(StatEnum.Intiligenz), new Stat(StatEnum.Spirit), new Stat(StatEnum.Stammina), new Stat(StatEnum.Armor), new Stat(StatEnum.Damage) };

    public StatList()
    {
        agility = stats[0];
        strength = stats[1];
        intiligenz = stats[2];
        spirit = stats[3];
        stammina = stats[4];
        armor = stats[5];
        damage = stats[6];
    }
}

[System.Serializable]
public class Stat
{
    public StatEnum statName;    
    [SerializeField] private int baseValue = 1;
    [SerializeField] private int valueMultipli = 0;


    public Stat(StatEnum se)
    {
        statName = se;
    }

    private List<int> modifers = new List<int>();
    public int GetValue()
    {
        int finalValue = baseValue;

        modifers.ForEach(m => finalValue += m);
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifers.Remove(modifier);
    }
}
