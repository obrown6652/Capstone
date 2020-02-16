﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{

    private static BagScript instance;

    public static BagScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BagScript>();
            }
            return instance;
        }
    }

    //prefab for creating slots
    [SerializeField]
    private GameObject slotPrefab;

    private CanvasGroup canvasGroup;

    private List<SlotScript> slots = new List<SlotScript>();

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    public List<SlotScript> MySlots { get { return slots; } }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach (SlotScript slot in MySlots)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }
            return count;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public List<Item> GetItems()
    {
        List<Item> items = new List<Item>();
        foreach (SlotScript slot in slots)
        {
            if (!slot.IsEmpty)
            {
                foreach (Item item in slot.MyItems)
                {
                    items.Add(item);
                }
            }
        }
        return items;
    }
    //creats slots for this bag
    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
          SlotScript slot =  Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            slot.MyBag = this;
          MySlots.Add(slot);
        }
    }
    public bool AddItem (Item item)
    {
        foreach (SlotScript slot in MySlots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                return true;
            }
        }
        return false;
    }
    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0: 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void Clear()
    {
        foreach (SlotScript slot in slots)
        {
            slot.Clear();
        }
    }
}
