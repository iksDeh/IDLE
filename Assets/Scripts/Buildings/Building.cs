using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{

    List<Door> doors;
    Door exitDoor;

    void Start()
    {      

    }

    public void UpdateDoors()
    {
        foreach (Door door in this.GetComponentsInChildren<Door>())
        {
            if (door.isExitDoor)
            {
                if (door.gameObject.activeInHierarchy)
                {
                    exitDoor = door;
                    //door.ResetDoor();
                    door.gameObject.SetActive(false);
                }
            }
        }
        foreach (Door door in this.GetComponentsInChildren<Door>())
        {
            if (door.isNew)
            {
                door.SetExitDoor(exitDoor);
            }
        }
    }
}


