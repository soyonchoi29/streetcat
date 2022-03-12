using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    sleep,
    idle,
    walk,
    hurt,
    interact
}

public class Player : MonoBehaviour
{
    private PlayerState currentState;
    
    [Header("Movement")]
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    public Animator animator;

    [Header("Player Health")]
    public GameObject healthBar;
    private Slider healthSlider;
    public int maxHealth;
    private int currentHealth;

    [Header("Player Hunger")]
    public GameObject hungerBar;
    private Slider hungerSlider;
    public int maxHunger;
    private float currentHunger;
    public SignalSender ateSomething;

    [Header("Items")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.idle; 
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        // Health bar code
        healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.maxValue = maxHealth = 200;
        healthSlider.value = currentHealth;
        currentHealth = maxHealth;

        // Hunger bar code
        hungerSlider = hungerBar.GetComponent<Slider>();
        hungerSlider.maxValue = maxHunger = 200;
        hungerSlider.value = currentHunger;
        currentHunger = maxHunger = 200;

        // Inventory
        playerInventory.currentItem = null;
        playerInventory.items.Capacity = 1;
        playerInventory.items.Clear();
    }

    //Update changed to FixedUpdate so character movement is smooth
    void FixedUpdate()
    {
        healthSlider.value = currentHealth;
        hungerSlider.value = currentHunger;

        if (currentState == PlayerState.interact)
        {
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        animator.SetInteger("state", (int)currentState);

        if (currentHunger == 0)
        {
            TooHungry();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && playerInventory.currentItem != null)
        {
            ReceiveItem();
            ateSomething.Raise();
        }

        else if (currentHunger > 0)
        {
            currentHunger -= (float)(Time.unscaledDeltaTime);
            hungerSlider.value = currentHunger;
        }
    }

    //Allows for 4-way animations
    void UpdateAnimationAndMove()
    {
        if (currentState != PlayerState.hurt && currentState != PlayerState.interact)
        {
            if (change != Vector3.zero)
            {
                MoveCharacter();
                animator.SetFloat("moveX", change.x);
                animator.SetFloat("moveY", change.y);

                currentState = PlayerState.walk;
            }
            else
            {
                currentState = PlayerState.idle;
            }
        }
    }

    //Moves character with arrow keys, can be used to add touchscreen controls later on
    void MoveCharacter()
    {
        change.Normalize();
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
    }

    void ReceiveItem()
    {
        currentHealth += playerInventory.currentItem.itemHealth;
        currentHunger += playerInventory.currentItem.itemHunger;
        playerInventory.currentItem = null;
        playerInventory.items.Clear();
        receivedItemSprite.sprite = null;
    }

    void TooHungry()
    {
            currentHealth -= 10;
    }

    // Collisions with cars and getting hurt
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Car"))
        {
            myRigidBody.isKinematic = true;
            myRigidBody.velocity = Vector3.zero;
            currentHealth -= 20;
            if (currentHealth > 0.01)
            {
                currentState = PlayerState.hurt;
                StartCoroutine("WaitAfterHurt");
            }
        }
    }

    // Waiting after getting hurt for player state to return to idle
    private IEnumerator WaitAfterHurt()
    {
        yield return new WaitForSecondsRealtime((float)1.6);
        currentState = PlayerState.idle;
        myRigidBody.isKinematic = false;
        myRigidBody.velocity = Vector3.zero;
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
                currentState = PlayerState.idle;
            }
            else
            {
                currentState = PlayerState.idle;
                playerInventory.currentItem = null;
            }
        }
    }
}
