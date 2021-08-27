using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    enum shape
    {
        Circle,
        Cube

    }    

    public Transform interactionTransform;
    [SerializeField] shape Shape;
    public float offsetX = 0f;
    public float offsetY = 0f;

    public float radius = 3f;
    public float height = 1;
    public float weight = 1;

    public bool enteredInteractRange { get; private set; } = false;
    public bool DrawCollisionArea = true;

    private BoxCollider2D bc2d;
    private CircleCollider2D cc2d;

    public float interact { get; private set; }
   
    public bool GetPlayerInteract()
    {
        return PlayerController.instance.GetInteract();
    }

    public virtual void StartInteract ()
    {
        foreach (Transform obj in GetComponentInChildren<Transform>())
            if (obj.name == "Text")
                obj.gameObject.SetActive(true);
    }

    public virtual void StayInteract ()
    {

    }

    public virtual void ExitInteract()
    {
        foreach (Transform obj in GetComponentInChildren<Transform>())
            if (obj.name == "Text")
                obj.gameObject.SetActive(false);
    }




    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {

    }
 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enteredInteractRange)
        {

            StartInteract();
            enteredInteractRange = true;

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (enteredInteractRange)
        {

            ExitInteract();
            enteredInteractRange = false;

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

            StayInteract();
    }

    

    private void OnDrawGizmosSelected()
    {
        if (DrawCollisionArea)
        {

            if (GetComponent<CircleCollider2D>() != null)
                cc2d = GetComponent<CircleCollider2D>();
            if (GetComponent<BoxCollider2D>() != null)
                bc2d = GetComponent<BoxCollider2D>();
            

            shape s = Shape;
            if (interactionTransform == null)
                interactionTransform = transform;
            Gizmos.color = Color.yellow;
            switch (s)
            {
                case shape.Circle:
                    DestroyImmediate(bc2d);
                    Gizmos.DrawSphere(interactionTransform.position + new Vector3(offsetX, offsetY, 0), radius);



                    break;
                case shape.Cube:
                    DestroyImmediate(cc2d);
                    Gizmos.DrawCube(interactionTransform.position + new Vector3(offsetX, offsetY, 0), new Vector3(weight, height, 0));


                    break;
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context )
    {
        interact = context.ReadValue<float>();
    }

}
