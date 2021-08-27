using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecepieMaterialUI : MonoBehaviour
{
    public GameObject material;
    public GameObject amount;

    public bool activSelf { get; private set; } 
    public void Init(GameObject mats, GameObject amo)
    {
        activSelf = false;
        material = mats;
        amount = amo;
    }

    public void SetActiv(bool value)
    {
        activSelf = value;
        material.SetActive(value);
        amount.SetActive(value);
    }

}
