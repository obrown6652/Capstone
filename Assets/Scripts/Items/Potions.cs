using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType {Health, Magic, Strength, Speed}
public class Potions : Item, IUseable

{

    
    //function use to activate the potion
    public virtual void Use()
    {
       
    }

    //the desritption of the potion
    public override string GetDescription()
    {
        return null;
    }
}
