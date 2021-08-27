using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRightClickMenu : ClickableObject
{


    public InventoryButtons invButtons;

    private void Start()
    {

    }
}

public enum InventoryButtons
{
    Use,
    Info,
    DeleteAmount,
    DeleteAll
}