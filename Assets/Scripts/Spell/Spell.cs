using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections;


public enum SpellType { AOE, PROJECTILE, SUMMON }

//This class whole all the infomation each type of spell have
[Serializable]
public abstract class Spell: ScriptableObject, IUseable, IMoveable
{
    Player player;

    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;



    [SerializeField]
    private float castTime;

    [SerializeField]
    private string Description;


    [SerializeField]
    private int magicCost;

    public string MyName
    {
        get
        {
            return name;
        }
    }
    public int MyDamage
    {
        get
        {
            return damage;
        }
    }
    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }
  
    public float MyCastTime
    {
        get
        {
            return castTime;
        }
        set
        {
            castTime = value;
        }
    }

    public string MyDescription
    { get { return Description; } }

    public int MagicCost
    {
        get
        {
            return magicCost;
        }
        set {
            magicCost = value;
        }
    }

    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }

    public virtual IEnumerator Ability()
    {
        yield return new WaitForSeconds(0);

    }

  
}