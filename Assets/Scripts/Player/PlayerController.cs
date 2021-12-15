using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DragonBones;

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



    public Ability part;

    public GameObject player;
    public Camera cam;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer[] sr;

    public Quest quest;
    public PlayerStats stats;
    public float movementSpeed = 5f;

    private float castDuration = 0;
    private float timePast = 0;
    private Vector2 movement;
    private float run = 0;
    private float interact = 0;
    private float inveontoryUI = 0;
    private float autoAttack = 0;
    private float onSpell1 = 0;
    UnityArmatureComponent playeranim;

    private bool animationChanged = false;
    private bool idle = false;
    // Start is called before the first frame update
    void Start()
    {
        particleList = new List<ParticleSystem>();
        playeranim = GetComponentInChildren<UnityArmatureComponent>();
        sr = GetComponentsInChildren<SpriteRenderer>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        StartCoroutine(HandleAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        Move();
        Cast();
        timePast += Time.deltaTime;
        if (castDuration > 0)
            castDuration -= Time.deltaTime;
        if (castDuration < 0)
            castDuration = 0;

        if (movement == new Vector2(0, 0) && castDuration <= 0)
            idle = true;
        else
            idle = false;
    }
    List<ParticleSystem> particleList;
    public void Cast()
    {
        if (onSpell1 > 0)
        {
            ParticleSystem p = Instantiate(part.particle, player.transform.position, Quaternion.Euler(player.transform.rotation.eulerAngles));
            StartCoroutine(HandleSpells(p, p.main.startLifetimeMultiplier));
            //part.particle.transform.position = player.transform.position;
            //part.particle.transform.rotation = player.transform.rotation;
            //part.particle.Play();
            p.Play();
            playeranim.animation.Play("Spell1", 1);
            castDuration = p.main.duration;
            animationChanged = true;
            timePast = 0;
            onSpell1 = 0;
            particleList.Add(p);
        }
    }
    public void Move()
    {
        if (movement != new Vector2(0, 0))
        {
            foreach (ParticleSystem p in particleList)
                p.Stop();

            float newMovementSpeed = movementSpeed;
            if (run == 1) newMovementSpeed *= 1.4f;
            if (movement.x < 0)
                player.transform.eulerAngles = new Vector3(0, 180, 0);
            else
                    player.transform.eulerAngles = new Vector3(0, 0, 0);

            rb.MovePosition(rb.position + movement * newMovementSpeed * Time.deltaTime);
            //this.transform.position = new Vector3(movement.x, movement.y, 0);
            //if (run == 1)
            //    playeranim.animation.Play("Walk",1);
            ////animator.SetBool("isRunning", true);
            //else
            //    playeranim.animation.Play("Walk",1);
            ////animator.SetBool("isWalking", true);

            animationChanged = true;
        }
        else if (movement == new Vector2(0, 0) && idle == false)
        {
            animationChanged = true;
            idle = true;
        }
    }

    IEnumerator HandleAnimation()
    {
        while(true)
        {
            yield return new WaitUntil(AnimationChanged);
            if (idle == true)
            {
                if(!playeranim.animation.lastAnimationName.Equals("Idle"))
                playeranim.animation.Play("Idle");
            }
            else if (movement != new Vector2(0, 0))
            {
                if (!playeranim.animation.lastAnimationName.Equals("Walk"))
                    playeranim.animation.Play("Walk");
            }
            else if(onSpell1 > 0)
            {
                if (!playeranim.animation.lastAnimationName.Equals("Spell1"))
                    playeranim.animation.Play("Spell1");
            }
            animationChanged = false;
        }

       // else if
    }
    private bool AnimationChanged()
    {
        return animationChanged;
    }
    public void EnemyKilled(Enemy enemy)
    {
        if (enemy != null)
        {
            stats.OnKill(enemy);
            Inventory.instance.Add(enemy.enemyLoot.GetItems());
            QuestManager.instance.UpdateQuest(enemy);
        }

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

    public void OnSpell1(InputAction.CallbackContext context)
    {
        if (part.cooldown <= timePast)
            onSpell1 = context.ReadValue<float>();
    }

    IEnumerator HandleSpells(ParticleSystem p, float duration)
    {

        yield return new WaitForSeconds(duration);
        particleList.Remove(p);
        Destroy(p.gameObject);

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
