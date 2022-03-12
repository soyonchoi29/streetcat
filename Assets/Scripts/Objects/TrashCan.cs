using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : PickUpAble
{
    public Item contents;
    public ItemSpawnTable spawnTable;
    public Inventory playerInventory;
    public SignalSender signalReceivedItem;
    private bool isDepleted;
    private int counter;

    private void Start()
    {
        counter = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange && playerInventory.items.Count == 0)
        {
            if (!isDepleted)
            {
                SpawnItem();
            }
            else
            {
                TrashCanDepleted();
            }
        }
    }

    private void SpawnItem()
    {
        if (spawnTable != null)
        {
            Item spawnTableOutcome = spawnTable.SpawnedItem();
            if (spawnTableOutcome != null)
            {
                contents = spawnTableOutcome;
                SearchTrashCan();
            }
        }
    }

    public void SearchTrashCan()
    {
        playerInventory.currentItem = contents;
        playerInventory.AddItem(contents);
        contextOff.Raise();
        signalReceivedItem.Raise();
        counter++;
        if (counter >= 3)
        {
            isDepleted = true;
        }
        else
        {
            isDepleted = false;
        }
    }

    public void TrashCanDepleted()
    {
        playerInventory.currentItem = null;
        signalReceivedItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isDepleted && playerInventory.currentItem == null)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isDepleted)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }

    public void AteSomethingButStillInRange()
    {
        if (playerInRange && !isDepleted)
        {
            contextOn.Raise();
        }
    }
}
