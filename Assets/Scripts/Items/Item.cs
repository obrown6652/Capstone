using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//superclass for all the items
public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stacksSize;

    private SlotScript slot;

    [SerializeField]
    private string title;

    [SerializeField]
    private float cost;

    [SerializeField]
    private Quality quality;

    [SerializeField]
    private float WeaponAnimationSpeed; //Make a weapon script for this so not all items have an animation

    public CharButton MyCharButton { get; set; }
    

    public float MyWeaponAnimationSpeed
    {
        get
        {
            return WeaponAnimationSpeed;
        }
    }
    public Sprite MyIcon {
        get
        {
            return icon;
        }
    }

    public int MyStackSize {
        get
        {
            return stacksSize;
        }
    }

    public SlotScript MySlot {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }

    public string MyTitle {
        get
        {
            return title;
        }
    }

    public float MyCost
    {
        get {
            return cost;
        }
        set {
            cost = value;
        }
    }

    public Quality MyQuality { get { return quality; } set { quality = value; } }

    public virtual string GetDescription()
    {

       

        return string.Format("<color={0}>{1}</color>", QualityColor.MyColor[quality],title);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
         //   MySlot = null;
        }
    }



}
