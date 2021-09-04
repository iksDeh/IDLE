using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "Inventory/Currency")]
public class Currency : Item
{
    public int hirachyID;
    public int maxCurrency = 1000;
    [HideInInspector] public int amount;
    //public int copper = 0;
    //public int silver = 0;
    //public int gold = 0;
    //public int platin = 0;

    //public Sprite imageCopper;
    //public Sprite imageSilver;
    //public Sprite imageGold;
    //public Sprite imagePlatin;

    //public Currency(int copper = 0, int silver = 0, int gold = 0, int platin = 0)
    //{
    //    this.copper = copper;
    //    this.silver = silver;
    //    this.gold = gold;
    //    this.platin = platin;
    //}

    //public void AddCurrency(Currency currency)
    //{
    //    copper += currency.copper;
    //    if (copper >= 100)
    //    {
    //        silver++;
    //        copper -= 100;
    //    }

    //    silver += currency.silver;
    //    if (silver >= 100)
    //    {
    //        gold++;
    //        silver -= 100;
    //    }

    //    gold += currency.gold;
    //    if (gold >= 100)
    //    {
    //        platin++;
    //        gold -= 100;
    //    }

    //    platin += currency.platin;
    //}

    //public bool RemoveCurrency(Currency currency)
    //{
    //    if ((this.copper - currency.copper) >= 0)
    //    {
    //        this.copper -= currency.copper;
    //        return true;
    //    }
    //    else if ((this.copper - currency.copper) < 0 && (this.silver > 0 || this.gold > 0 || this.platin > 0))
    //    {
    //        if (this.silver > 0)
    //        {
    //            this.silver -= 1;
    //            this.copper += 100 - currency.copper;
    //        }
    //        else if (this.gold > 0)
    //        {
    //            this.gold -= 1;
    //            this.silver += 99;
    //            this.copper += 100 - currency.copper;
    //        }
    //        else if (this.platin > 0)
    //        {
    //            this.platin -= 1;
    //            this.gold += 99;
    //            this.silver += 99;
    //            this.copper += 100 - currency.copper;
    //        }
    //        return true;
    //    }
    //    if ((this.silver - currency.silver) >= 0)
    //    {
    //        this.silver -= currency.silver;
    //        return true;
    //    }
    //    else if ((this.silver - silver) < 0 && (this.gold > 0 || this.platin > 0))
    //    {
    //        if (this.gold > 0)
    //        {
    //            this.gold -= 1;
    //            this.silver += 100 - currency.silver;
    //        }
    //        else if (this.platin > 0)
    //        {
    //            this.platin -= 1;
    //            this.gold += 99;
    //            this.silver += 100 - currency.silver;
    //        }
    //        return true;
    //    }
    //    if ((this.gold - gold) >= 0)
    //    {
    //        this.gold -= gold;
    //        return true;
    //    }
    //    else if ((this.gold - currency.gold) < 0 && this.platin > 0)
    //    {
    //        this.platin -= 1;
    //        this.gold += 100 - currency.gold;
    //        return true;
    //    }
    //    return false;
    //}
}
