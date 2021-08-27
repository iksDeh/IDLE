using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    private SpriteRenderer sr;
    public Item item;
    public GameObject itemGFX;
    private MeshRenderer[] mr;

    private TextMesh tm;
    PlayerController playerController;


    public void Start()
    {
        if (itemGFX != null)
        {
            sr = itemGFX.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = item.icon;

            mr = itemGFX.GetComponentsInChildren<MeshRenderer>();
            foreach (TextMesh t in itemGFX.GetComponentsInChildren<TextMesh>())
            {
                if (t.text == "")
                    tm = t;
            }
        }
        playerController = PlayerController.instance;
    }
    public override void StartInteract()
    {
        base.StartInteract();

        foreach (MeshRenderer m in mr)
            m.enabled = true;

        tm.text = item.name;
    }

    public override void ExitInteract()
    {
        base.ExitInteract();

        foreach (MeshRenderer m in mr)
            m.enabled = false;

        tm.text = "";
    }

    public override void StayInteract()
    {
        base.StayInteract();
        if (GetPlayerInteract())
            PickUp();

    }

    void PickUp()
    {
        Debug.Log("Pick up " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }

}
