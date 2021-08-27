using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : Interactable
{
    public bool onFloor { get; private set; } = true;

    public override void StartInteract()
    {
        base.StartInteract();
        onFloor = true;
    }

    public override void ExitInteract()
    {
        base.ExitInteract();
        onFloor = false;
    }
}
