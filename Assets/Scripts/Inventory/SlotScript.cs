using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler,IClickable, IPointerEnterHandler, IPointerExitHandler
{

    private static SlotScript instance;

    public static SlotScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SlotScript>();
            }
            return instance;
        }
    }

    private ObservableStack<Item> items  = new ObservableStack<Item>();

    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

    //A reference to the bag that this slot belong to
    public BagScript MyBag { get; set; }

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool IsEmpty
    {
        get {
            return items.Count == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            return true;
        }
    }
    public Item MyItem
    {
        get {
            if (!IsEmpty)
            {
                return items.Peek();
            }
            return null;
        }
    }

    public Image MyIcon {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return items.Count;
        }

    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public bool AddItem(Item item)
    {

        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }
    public bool AddItems(ObservableStack<Item> newItmes)
    {
        if (IsEmpty || newItmes.Peek().GetType() == MyItem.GetType())
        {
            int count = newItmes.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItmes.Pop());
            }
            return true;

        }
        return false;
    }
    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }

    public void Clear()
    {
        if (items.Count > 0)
        {
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            items.Clear();
        }
    }

    //if mouse is click on inventory slots
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty)//if we don't have something to move
            {
                if (HandScript.MyInstance.MyMoveable != null)
                {
                    if (HandScript.MyInstance.MyMoveable is Bag)
                    {
                        
                        if (MyItem is Bag)
                        {
                            InventoryScript.MyInstance.SwapBag(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        } 
                    }
                    else if (HandScript.MyInstance.MyMoveable is Armor)
                    {
                        if (MyItem is Armor && (MyItem as Armor).MyArmorType == (HandScript.MyInstance.MyMoveable as Armor).MyArmorType)
                        {
                            (MyItem as Armor).Equip();
                            HandScript.MyInstance.Drop();
                        }
                    }


                }
                else
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.MyInstance.FromSlot = this;
                }
               
            }
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
            {
                if (HandScript.MyInstance.MyMoveable is Bag)
                {
                    
                    //Dequips a bag for the inventory 
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                    //make sure we can't dequip it into itslef and that we have enough space for the items form the dequipped bag
                    if (bag.MyBagScript != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - bag.Slots > 0)
                    {

                        AddItem(bag);
                        bag.MyBagButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                    } 
                }
                else if (HandScript.MyInstance.MyMoveable is Armor)
                {
                    Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
                    AddItem(armor);
                    CharacterPanel.MyInstance.MySlectedButton.DequipArmor();
                    HandScript.MyInstance.Drop();

                }
              
            }
            else if (InventoryScript.MyInstance.FromSlot != null) //if we have something to move
            {
                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot)|| SwapItems(InventoryScript.MyInstance.FromSlot)|| AddItems(InventoryScript.MyInstance.FromSlot.items))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
       
        }


        if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null)
        {
            UseItem();
           
        }
    }

    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
        else if (MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }

    private bool PutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }
    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
        {
            //copy all the items we need to swap from A
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>( from.items);

            //clear slot A
            from.items.Clear();
            //all items from slot B and copy them into A
            from.AddItems(items);

            //clear B
            items.Clear();
            //move the items from A copy to B
            AddItems(tmpFrom);

            return true;

        }
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            //how many free slots in the stack
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.items.Pop());
            }
            return true;
        }
        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //show tooltip
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip(transform.position, new Vector2(1, 1), MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //hide tool tip
        UIManager.MyInstance.HideTooltip();
    }
}
