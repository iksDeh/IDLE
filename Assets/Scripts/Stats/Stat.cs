using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

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
