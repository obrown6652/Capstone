using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
 

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openSprite, closeSprite, interactSprite;

    private bool isOpen;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private List<Item> items;

    [SerializeField]
    private BagScript bag;

    [SerializeField]
    private Loot[] loot;

    private List<Item> droppedItems = new List<Item>();

    private bool doCreate;

    private void Start()
    {
        //CreateLoot();

        doCreate = true;
    }
    
    public void CreateLoot()
    {
        foreach (Loot item in loot)
        {
            int roll = Random.Range(0, 100);

            if (roll <= item.MyDropChance)
            {
                droppedItems.Add(item.MyItem);
            }
        }

        foreach (Item item in droppedItems)
        {
           bag.AddItem(item);

        }
    }
     


    public void Interact()
    {
        if (isOpen)
        {
             CloseChest();
            StoreItem();
            //StopInteract();
        }
        else
        {
            if (doCreate)
            {
                CreateLoot();
                doCreate = false;
            }
           
           
            AddItems();
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;

        }
    }

    public void StopInteract()
    {

        StoreItem();
        bag.Clear();
        CloseChest();
    }

    public void CloseChest()
    {
        
        isOpen = false;
        spriteRenderer.sprite = closeSprite;

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

    }

    public void AddItems()
    {
        Debug.Log("add item back");
        if (items != null)
        {
            foreach (Item item in items)
            {
                item.MySlot.AddItem(item);
            }
        }
    }

    public void StoreItem()
    {
        items = bag.GetItems();

    }

}
