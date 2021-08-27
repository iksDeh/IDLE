using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    #region Singelton

    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("!!! More than one instance of CraftingManager found !!!");

        instance = this;
    }

    #endregion

    public event System.Action OnUIChanged;

    public Transform playerUI;

    public Transform invetorUI;
    public Transform equipmentUI;
    public Transform craftingUI;

    //public Transform vendorUI;
    //public Transform questGiveUI;
    public Transform questUI;

    void Start()
    {
        
    }



}
