using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Door : Interactable
{
    public bool isExit;

    public List<Transform> activOnEnter;
    public List<Transform> inActiveOnEnter;
    public Transform floorForIsExit;

    public bool isExitDoor { get; private set; } = false;

    private Building building;
    private Door door;

    private bool isActiv = false;
    private bool isTriggerd = false;
    public bool isNew { get; private set; } = false;

    public event System.Action OnDoorStatusChanged;
    void Start()
    {
        building = this.GetComponentInParent<Building>();
        OnDoorStatusChanged += building.UpdateDoors;

        if(activOnEnter.Count == 0)
        {
            Debug.Log("ERROR: List not Defiend in 'Door' Settings");
            activOnEnter = new List<Transform>();
        }
        if (inActiveOnEnter.Count == 0)
        {
            Debug.Log("ERROR: List not Defiend in 'Door' Settings");
            inActiveOnEnter = new List<Transform>();
        }
        if(isExit)
        if(floorForIsExit == null)
            Debug.Log("ERROR: 'Floor' not Defiend in 'Door' Settings");
        //onDoorOpened += OpenDoor;
    }

    public void SetExitDoor(Door door)
    {
        this.door = door;
    }

    public override void StartInteract()
    {
        base.StartInteract();
        if(!isActiv && !isTriggerd)
        {

            foreach (Transform trans in inActiveOnEnter)
            {
                foreach (Transform t in trans)                   
                    if (t != this.transform)
                    {
                        t.gameObject.SetActive(false);
                    }

            }
            foreach (Transform t in activOnEnter)
            {
                t.gameObject.SetActive(true);
                foreach(Transform trans in t)
                    trans.gameObject.SetActive(true);
            }

            isNew = true;
            if (OnDoorStatusChanged != null)
                OnDoorStatusChanged();
            isNew = false;

            isExitDoor = true;
            isTriggerd = true;
            isActiv = true;
        }
        else if(!isTriggerd)
        {
            if (!isExit)
            {

                foreach (Transform trans in activOnEnter)
                {
                    foreach (Transform t in trans)
                        if (t != this.transform)
                            t.gameObject.SetActive(false);
                }

                foreach (Transform t in inActiveOnEnter)
                    if (t != this.transform)
                    {
                        t.gameObject.SetActive(true);
                        foreach (Transform trans in t)
                            trans.gameObject.SetActive(true);
                    }



                isExitDoor = false;
                if (OnDoorStatusChanged != null)
                    OnDoorStatusChanged();

                door.GetComponent<Transform>().gameObject.SetActive(true);

                isActiv = false;
                isTriggerd = true;
            }
        }
    }

    public override void ExitInteract()
    {
        base.ExitInteract();
        if ( isExit && !floorForIsExit.GetComponent<Floor>().onFloor)
        {
            foreach (Transform trans in activOnEnter)
            {
                foreach (Transform t in trans)
                    if (t != this.transform)
                        t.gameObject.SetActive(false);
            }

            foreach (Transform t in inActiveOnEnter)
                if (t != this.transform)
                {
                    t.gameObject.SetActive(true);
                    foreach (Transform trans in t)
                        trans.gameObject.SetActive(true);
                }
            isExitDoor = false;
            isActiv = false;
        }
        isTriggerd = false;
    }

    public void ResetDoor()
    {
        isExitDoor = false;
    }

}
