using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ArmorType {Outfit, MainHand, Accessory}
enum weaponType {None,Sword,Staff,Gun }

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]

public class Armor : Item
{
    private static Armor instance;

    public static Armor MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Armor>();
            }
            return instance;
        }
    }
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private weaponType weaponType;

    internal weaponType MyWeaponType { get { return weaponType; } }

    [SerializeField]
    private int health;
    public int MyHealth
    {
        get { return health; }
    }

    [SerializeField]
    private int healthRecovery;
    public int MyHealthRecovery
    {
        get { return healthRecovery; }
    }

    [SerializeField]
    private int magic;
    public int MyMagic
    {
        get { return magic; }
    }

    [SerializeField]
    private int magicRecovery;
    public int MyMagicRecovery
    {
        get { return magicRecovery; }
    }

    [SerializeField]
    private int magicDamage;
    public int myMagicDamage
    {
        get { return magicDamage; }
    }


    [SerializeField]
    private int physicalDamage;
    public int MyphsicalDamage
    {
        get { return physicalDamage; }
    }

    [SerializeField]
    private int Resistance;
    public int MyResistance
    {
        get { return Resistance; }
    }

    [SerializeField]
    private int speed;
    public int MySpeed
    {
        get { return speed; }
    }


    [SerializeField]
    private AnimationClip[] animationClips;

    public AnimationClip[] MyAnimationClips
    {
        get
        {
            return animationClips;
        }
    }
    internal ArmorType MyArmorType
    {
        get
        {
            return armorType;
        }
    }

    [SerializeField]
    private string Description;

    public override string GetDescription()
    {
        if (Description == string.Empty)
        {
            Description = "There is no description";
        }

        //split description and break new line after 9 words
        string[] splitPhases = Description.Split(' ');
        string newPhase = null;
        int count = 0;
        foreach (var splitPhase in splitPhases)
        {
            if (count == 9)
            {
                Debug.Log("do split");
                newPhase += Environment.NewLine;
                count = 0;
            }

            newPhase = newPhase + splitPhase+ " ";
            count++;
           
        }
        return (newPhase);
    }

    public string GetStats()
    {
        string stats = string.Empty;

        if (health > 0)
        {
<<<<<<< HEAD
<<<<<<< Updated upstream
            stats += string.Format("\n +{0} health",health);
=======
            stats += string.Format("\n +{0} health", Player.MyInstance.MyHealth.MyMaxValue - health);
>>>>>>> Stashed changes
=======
            stats += string.Format("\n +{0} health", health);
>>>>>>> master
        }
        if (healthRecovery > 0)
        {
            stats += string.Format("\n +{0} health Recovery", Player.MyInstance.MyHealthREgenerationStat.MyMaxValue -healthRecovery);
        }
        if (magic > 0)
        {
            stats += string.Format("\n +{0} magic", Player.MyInstance.MyMana.MyMaxValue - magic);
        }
        if (magicRecovery > 0)
        {
            stats += string.Format("\n +{0} magic Recovery", Player.MyInstance.MyMagicRegenerationStat.MyMaxValue - magicRecovery);
        }
        if (magicDamage > 0)
        {
            stats += string.Format("\n +{0} spell Damage", Player.MyInstance.MyMagicStrengthStat.MyMaxValue - magicDamage);
        }
        if (physicalDamage > 0)
        {
            stats += string.Format("\n +{0} physical Damage", Player.MyInstance.MyPhysicalStrengthStat.MyMaxValue - physicalDamage);
        }
        if (Resistance > 0)
        {
            stats += string.Format("\n +{0} Resistance", Player.MyInstance.MyResistanceStat.MyMaxValue - Resistance);
        }
        if (speed > 0)
        {
            stats += string.Format("\n +{0} Speed", Player.MyInstance.MyMovementSpeed.MyMaxValue - speed);
        }
        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        CharacterPanel.MyInstance.EpuipArmor(this);
    }
}
