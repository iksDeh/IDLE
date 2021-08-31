using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Singelton

    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("!!! More than one instance of Inventory found !!!");

        instance = this;
    }

    #endregion




    public GameObject player;
    public Camera cam;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer[] sr;

    public Quest quest;
    public PlayerStats stats;
    public float movementSpeed = 5f;

    private Vector2 movement;
    private float run = 0;
    private float interact = 0;
    private float inveontoryUI = 0;
    private float autoAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (movement != new Vector2(0, 0))
        {
            float newMovementSpeed = movementSpeed;
            if (run == 1) newMovementSpeed *= 1.4f;
            if (movement.x < 0)
                player.transform.eulerAngles = new Vector3(0, 180, 0);
            else
                    player.transform.eulerAngles = new Vector3(0, 0, 0);

            rb.MovePosition(rb.position + movement * newMovementSpeed * Time.deltaTime);
            //this.transform.position = new Vector3(movement.x, movement.y, 0);
            if (run == 1)
                animator.SetBool("isRunning", true);
            else
                animator.SetBool("isWalking", true);
        }
        else if (movement == new Vector2(0, 0))
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    public void EnemyKilled(List<int> questIds)
    {
            if (questIds.Count > 0)
            QuestManager.instance.UpdateQuest(questIds);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();


    }
    public void OnRun(InputAction.CallbackContext context)
    {
        run = context.ReadValue<float>();

    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        interact = context.ReadValue<float>();

    }

    public void OnAutoAttack(InputAction.CallbackContext context)
    {
        autoAttack = context.ReadValue<float>();
    }

    public void OnInventoryUI(InputAction.CallbackContext context)
    {
        inveontoryUI = context.ReadValue<float>();
        Debug.Log("Hallo");
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
    public Vector2 mousePos;

    public bool GetAutoAttack()
    {
        if (autoAttack > 0)
            return true;
        else
            return false;
    }
    public bool GetInventoryUI()
    {
        if (inveontoryUI > 0)
            return true;
        else
            return false;
    }

    public bool GetInteract()
    {
        if (interact > 0)
            return true;
        else
            return false;
    }

}
